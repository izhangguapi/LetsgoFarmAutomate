import win32gui

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
        print(win32gui.GetWindowText(hwnd),hwnd)
        if title in win32gui.GetWindowText(hwnd):
            result.append(hwnd)
    return result

find_windows_by_title("元梦")