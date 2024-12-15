import pyautogui
import time


# 获取鼠标位置
def get_mouse_positon():
    try:
        for i in range(10):
            # Get and print the mouse coordinates.
            x, y = pyautogui.position()
            positionStr = "鼠标坐标点（X,Y）为：{},{}".format(
                str(x).rjust(4), str(y).rjust(4)
            )
            print(positionStr)
            time.sleep(1)  # 停顿时间
    except:
        print("获取鼠标位置失败")

if __name__ == "__main__":
    get_mouse_positon()
