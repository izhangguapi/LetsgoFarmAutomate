from myTools import *
from identify import *
import os


def open_ymzx():
    """打开元梦"""
    print_log("打开元梦之星")
    os.system("open -a '元梦之星-云游戏-快捷方式'")
    sleep(1)


def find_drone():
    """寻找无人机"""
    # 重置位置后，按下a键一秒钟
    x, y = lgf_config["reset"]
    pyautogui.click(x=x, y=y, clicks=2, interval=0.5)
    print_log("寻找无人机")
    pyautogui.keyDown("a")
    sleep(1)
    pyautogui.keyUp("a")


def drone_work():
    """无人机工作"""
    find_drone()
    print_log("无人机开始工作")
    x, y = lgf_config["drone_btn"]
    pyautogui.leftClick(x=x, y=y)


def fishpond():
    """收获渔场"""
    find_drone()
    for _ in range(2):
        x, y = lgf_config["toggle_left"]
        pyautogui.click(x=x, y=y)
        sleep(0.5)
    try:
        print_log("正在识别鱼塘成熟时间...")
        screenshot_save("fishpond")
        time = OCR_time("fishpond")
        if time < 360:
            t = time - 2
            specific_time = datetime.datetime.now() + datetime.timedelta(seconds=t)
            print_log(
                "等待{}秒后开始钓鱼具体时间为{}".format(t, specific_time), "green"
            )
            sleep(t)
            drone_work()
            return True
        else:
            print_log("等待时间超过360秒，跳过", "red")
            drone_work()
            return False
    except:
        drone_work()
        return False


def start():
    print_log("开始运行", "green")
    while not exit_event.is_set():
        # open_ymzx()
        sleep(3)
        start_time = datetime.datetime.now()
        x, y = lgf_config["blank"]
        pyautogui.leftClick(x=x, y=y)
        pyautogui.click(x=x, y=y, clicks=5, interval=0.2)
        # 执行鱼塘工作
        if fishpond():
            start_time = datetime.datetime.now()
        # 计算耗时
        end_time = datetime.datetime.now()
        computation_time = end_time - start_time
        t = lgf_config["loops"] - computation_time.total_seconds()
        print_log(
            "本次任务耗时：{}秒，休息{}秒，".format(
                round((computation_time).total_seconds(), 2), t
            ),
            "green",
        )
        print_log(
            "下次运行时间：{}".format(end_time + datetime.timedelta(seconds=t)),
            "green",
        )
        sleep(t)

    print_log("程序被停止", "red")
