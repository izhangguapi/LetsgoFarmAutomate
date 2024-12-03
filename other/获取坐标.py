import win32gui, win32api
from pynput import mouse
from pynput import keyboard
from pynput.keyboard import Key


def get_mouse_pos_in_window(hwnd):
    # 获取鼠标屏幕坐标
    mouse_pos = win32api.GetCursorPos()

    # 将屏幕坐标转换为窗口坐标
    window_pos = win32gui.ScreenToClient(hwnd, mouse_pos)

    return window_pos


# 获取窗口句柄
hwnd = 854834

rect = win32gui.GetWindowRect(hwnd)
print(rect[2] - rect[0], rect[3] - rect[1])

def on_click(x, y, button, pressed):
    # 鼠标点击事件处理函数
    if pressed:
        print(get_mouse_pos_in_window(hwnd))
        # print('鼠标点击在 ({0}, {1})'.format(x, y))

def on_release(key):
    if key == Key.esc:
        # 停止监听
        return False

mouse_listener = mouse.Listener(
    # on_move=on_move,
    on_click=on_click
    # on_scroll=on_scroll
)
# 启动监听器
mouse_listener.start()

with keyboard.Listener(on_release=on_release) as listener:
    listener.join()