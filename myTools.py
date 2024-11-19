import datetime, threading, win32gui, win32api, win32con, win32ui, json, os, ctypes
from paddleocr import PaddleOCR
import win32clipboard as w
from ctypes import windll
from PIL import Image
import tkinter as tk


hwnd = None
title = None
exit_event = threading.Event()

lgf_config = {
    "farm_enable": True,  # 农场启用
    "farm_plots": 30,  # 农场地块数
    "pasture_enable": True,  # 牧场启用
    "pasture_plots": 15,  # 牧场地块数
    "fishpond_enable": True,  # 鱼塘启用
    "processors_enable": True,  # 加工期启用
    "loops": 538,  # 循环时间
    "vip": True,  # 是否开通农场月卡
}


class CalculationWaiting:
    """计算等待时间"""

    def __init__(self, seconds=530):
        self.time = seconds
        self.start_time = None
        self.end_time = None

    def set_now(self):
        self.start_time = datetime.datetime.now()

    def computing_time(self):
        self.end_time = datetime.datetime.now()
        time_difference = (self.end_time - self.start_time).total_seconds()
        s = lgf_config["loops"] - time_difference
        specific_time = self.computing_specific_time(time=self.end_time, seconds=s)
        print_log(f"本次任务耗时: {time_difference} 秒，休息 {s} 秒", "green")
        print_log(f"下次运行时间: {specific_time}", "green")
        sleep(s)

    def computing_specific_time(self, seconds, time=datetime.datetime.now()):
        return time + datetime.timedelta(seconds=seconds)


class WindowController:

    def __init__(self, hwnd):
        self.hwnd = hwnd

    def key_press(self, key: str, interval=0.1):
        """模拟一次按键的输入，间隔值默认0.1秒"""
        key = ord(key.upper())
        win32api.PostMessage(self.hwnd, win32con.WM_KEYDOWN, key, 0)
        sleep(interval)
        win32api.PostMessage(self.hwnd, win32con.WM_KEYUP, key, 0)

    def key_down(self, key: str):
        """模拟一个按键的按下"""
        key = ord(key.upper())
        win32api.PostMessage(self.hwnd, win32con.WM_KEYDOWN, key, 0)

    def key_up(self, key: str):
        """模拟一个按键的弹起"""
        key = ord(key.upper())
        win32api.PostMessage(self.hwnd, win32con.WM_KEYUP, key, 0)

    def mouse_move(self, position):
        """模拟鼠标的移动"""
        x, y = position
        point = win32api.MAKELONG(x, y)
        win32api.PostMessage(self.hwnd, win32con.WM_MOUSEMOVE, None, point)

    def mouse_up(self, position, button="L"):
        """模拟鼠标的按键抬起"""
        x, y = position
        button = button.upper()
        point = win32api.MAKELONG(x, y)
        if button == "L":
            win32api.PostMessage(
                self.hwnd, win32con.WM_LBUTTONUP, win32con.MK_LBUTTON, point
            )
        elif button == "R":
            win32api.PostMessage(
                self.hwnd, win32con.WM_RBUTTONUP, win32con.MK_RBUTTON, point
            )

    def mouse_down(self, position, button="L"):
        """模拟鼠标的按键按下"""
        x, y = position
        button = button.lower()
        point = win32api.MAKELONG(x, y)
        if button == "L":
            win32api.PostMessage(
                self.hwnd, win32con.WM_LBUTTONDOWN, win32con.MK_LBUTTON, point
            )
        elif button == "R":
            win32api.PostMessage(
                self.hwnd, win32con.WM_RBUTTONDOWN, win32con.MK_RBUTTON, point
            )

    def mouse_double(self, position):
        """模拟鼠标的左键双击"""
        x, y = position
        point = win32api.MAKELONG(x, y)
        win32api.PostMessage(
            self.hwnd, win32con.WM_LBUTTONDBLCLK, win32con.MK_LBUTTON, point
        )
        win32api.PostMessage(
            self.hwnd, win32con.WM_LBUTTONUP, win32con.MK_LBUTTON, point
        )

    def mouse_move_press(self, position):
        """模拟鼠标移动到坐标，并进行左键单击"""
        x, y = position
        point = win32api.MAKELONG(x, y)
        win32api.PostMessage(self.hwnd, win32con.WM_MOUSEMOVE, None, point)
        win32api.PostMessage(
            self.hwnd, win32con.WM_LBUTTONDOWN, win32con.MK_LBUTTON, point
        )
        win32api.PostMessage(
            self.hwnd, win32con.WM_LBUTTONUP, win32con.MK_LBUTTON, point
        )

    def mouse_move_press_double(self, position):
        """模拟鼠标移动到坐标，并进行左键双击"""
        x, y = position
        point = win32api.MAKELONG(x, y)
        win32api.PostMessage(self.hwnd, win32con.WM_MOUSEMOVE, None, point)
        win32api.PostMessage(
            self.hwnd, win32con.WM_LBUTTONDBLCLK, win32con.MK_LBUTTON, point
        )
        win32api.PostMessage(
            self.hwnd, win32con.WM_LBUTTONUP, win32con.MK_LBUTTON, point
        )

    def screenshot_window(self, position=None):
        """截取整个窗口，传参就是截取该区域的图片，"""
        # 如果使用高 DPI 显示器（或 > 100% 缩放尺寸），添加下面一行，否则注释掉
        # windll.user32.SetProcessDPIAware()

        left, top, right, bot = win32gui.GetClientRect(self.hwnd)
        w = right - left
        h = bot - top

        hwndDC = win32gui.GetWindowDC(self.hwnd)
        # 根据窗口句柄获取窗口的设备上下文DC（Divice Context）
        mfcDC = win32ui.CreateDCFromHandle(hwndDC)  # 根据窗口的DC获取mfcDC
        saveDC = mfcDC.CreateCompatibleDC()  # mfcDC创建可兼容的DC

        saveBitMap = win32ui.CreateBitmap()  # 创建bitmap准备保存图片
        saveBitMap.CreateCompatibleBitmap(mfcDC, w, h)  # 为bitmap开辟空间

        saveDC.SelectObject(saveBitMap)  # 高度saveDC，将截图保存到saveBitmap中

        # 选择合适的 window number，如0，1，2，3，直到截图从黑色变为正常画面
        result = windll.user32.PrintWindow(self.hwnd, saveDC.GetSafeHdc(), 3)
        # 从位图对象中保存图像
        bmpinfo = saveBitMap.GetInfo()
        bmpstr = saveBitMap.GetBitmapBits(True)

        img = Image.frombuffer(
            "RGB",
            (bmpinfo["bmWidth"], bmpinfo["bmHeight"]),
            bmpstr,
            "raw",
            "BGRX",
            0,
            1,
        )
        # 释放资源
        win32gui.DeleteObject(saveBitMap.GetHandle())
        saveDC.DeleteDC()
        mfcDC.DeleteDC()
        win32gui.ReleaseDC(self.hwnd, hwndDC)

        if result == 1:
            # print(position)
            # 如果有坐标就截取坐标
            if position:
                img = img.crop(position)
            # img.show()
            # 定义图片路径
            img_path = os.path.join(
                os.environ.get("TEMP"), get_datetime().replace(":", "_") + ".png"
            )
            # 保存图片并返回
            img.save(img_path)
            return img_path
        else:
            print("未能截取窗口")


def get_datetime():
    """获取当前日期和时间"""
    return str(datetime.datetime.now())


def get_position():
    """获取坐标json文件"""
    position = None
    with open("position.json", "r", encoding="utf-8") as file:
        position = json.load(file)
    return position


def get_key():
    """获取按键json文件"""
    key = None
    with open("key.json", "r", encoding="utf-8") as file:
        key = json.load(file)
    return key


def ocr_img(img_path):
    """
    识别图片中的时间，并将其转为秒
    参数:
    img(str): 图片名称,会自动添加

    返回值: 返回图片中时间转为秒，以及图片识别耗时
    """
    # 创建一个PaddleOCR对象
    ocr = PaddleOCR(show_log=False, lang="ch", use_angle_cls=False)
    # 使用PaddleOCR对象进行图片识别
    result = ocr.ocr(img_path, cls=False)
    print_log("识别结果：{}".format(result))
    result = result[0]
    list_text = []
    if result:
        for i in result:
            list_text.append(i[1][0])
    os.remove(img_path)
    return list_text


def sleep(s):
    """休眠的异常处理"""
    if s < 0:
        print_log("休眠时间小于0，跳过", "red")
        return
    elif not exit_event.is_set():
        exit_event.wait(s)
    else:
        print_log("停止挂机")
        exit()


def check_settings(name):
    enable = lgf_config[name + "_enable"]
    timer = lgf_config[name + "_timer"]
    if not enable:
        return False
    if enable and not timer:
        return True
    time = lgf_config[name + "_time"]
    now = datetime.datetime.now()
    print_log("当前时间：", now)
    print_log("设定时间：", time)
    farm_time = datetime.datetime.strptime(time, "%Y-%m-%d %H:%M:%S")
    if now < farm_time:
        return True
    else:
        print_log("超过设定时间，不予执行")
        return False


def convert_to_seconds(time):
    if "后" in time:
        time = time.split("后")[0]
        h, s, m = 0, 0, 0
        if "时" in time:
            temp = time.split("小时")
            h = int(temp[0])
            time = temp[1]
        if "分" in time:
            temp = time.split("分")
            m = int(temp[0])
            time = temp[1]
        if "秒" in time:
            s = int(time.split("秒")[0])
        seconds = h * 3600 + m * 60 + s
        print_log(f"转为秒：{seconds}")
        return seconds
    else:
        return 3


# def create_window(title):
#     """创建窗口"""
#     global window
#     window = tk.Tk()
#     window.title(title)
#     return window


# def create_progressbar():
#     """创建进度条"""
#     global p
#     p = ttk.Progressbar(window, maximum=300, value=0, mode="determinate")
#     p.grid(row=1, column=0, columnspan=4, padx=10, sticky="nsew")


# def create_button(text, row, column):
#     """创建按钮"""
#     btn = tk.Button(window, text=text)
#     btn.grid(row=row, column=column, pady=(0, 5))
#     return btn


# def btn_active(btn_list):
#     for btn in btn_list:
#         btn.config(state="active")


# def btn_disable(btn_list):
#     for btn in btn_list:
#         btn.config(state="disabled")


# def update_progressbar(num):
#     """更新进度条"""
#     p["value"] = p["value"] + num
#     window.update()


# def clear_progressbar():
#     """清除进度条"""
#     p["value"] = 0
#     window.update()


def create_listbox(window: tk.Tk):
    """创建列表框"""
    global lb
    lb = tk.Listbox(window, height=6, font=("Arial", 10))
    lb.grid(row=1, column=0, columnspan=6, sticky="nsew")


def delete_top_item():
    """超过100行就删除顶部的"""
    if len(lb.get(0, tk.END)) > 100:
        lb.delete(0, 0)


# 日志名称
log_name = None
current_path = os.path.dirname(os.path.abspath(__file__))


def save_log(text):
    """保存日志"""
    # 路径
    try:
        path = os.path.join(current_path, "log", log_name)
        with open(path, "a+", encoding="utf-8") as f:
            f.write(text + "\n")
    except:
        pass


def print_log(text, fg="black"):
    """打印日志"""
    log = "[" + get_datetime() + "] - " + str(text)
    try:
        lb.insert(tk.END, log)
        lb.itemconfigure(tk.END, fg=fg)
        lb.see(tk.END)
        delete_top_item()
    except:
        print(log)
    save_log(log)


def get_all_windows():
    """获取全部窗口句柄"""
    windows = []

    def callback(hwnd, windows):
        windows.append(hwnd)

    win32gui.EnumWindows(callback, windows)
    return windows


def find_windows_by_title(title):
    """根据标题寻找窗口"""
    windows = get_all_windows()
    result = []
    for hwnd in windows:
        # print(win32gui.GetWindowText(hwnd),hwnd)
        if title in win32gui.GetWindowText(hwnd):
            result.append(hwnd)
    return result
