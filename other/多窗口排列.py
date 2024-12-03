import win32gui
import win32con


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


hwnd = find_windows_by_title("元梦之星")


x = 0
y = -30
index = 1
hwnd.sort()
# hwnd.remove(196794)
if hwnd:
    for i in hwnd:
        win32gui.MoveWindow(i, x, y, 835, 500, False)
        x += 835
        if index % 3 == 0:
            x = 0
            y += 500 - 38
        index += 1
else:
    print("未找到窗口")
