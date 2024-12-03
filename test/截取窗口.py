import win32gui
import win32ui
from ctypes import windll
from PIL import Image


def photo_capture():

    # hwnd = win32gui.FindWindow(None, 'Melvor Idle')  # 获取窗口的句柄
    hwnd = 5442702  # 或设置窗口句柄

    # 如果使用高 DPI 显示器（或 > 100% 缩放尺寸），添加下面一行，否则注释掉
    # windll.user32.SetProcessDPIAware()

    left, top, right, bot = win32gui.GetClientRect(hwnd)
    w = right - left
    h = bot - top

    hwndDC = win32gui.GetWindowDC(hwnd)  # 根据窗口句柄获取窗口的设备上下文DC（Divice Context）
    mfcDC = win32ui.CreateDCFromHandle(hwndDC)  # 根据窗口的DC获取mfcDC
    saveDC = mfcDC.CreateCompatibleDC()  # mfcDC创建可兼容的DC

    saveBitMap = win32ui.CreateBitmap()  # 创建bitmap准备保存图片
    saveBitMap.CreateCompatibleBitmap(mfcDC, w, h)  # 为bitmap开辟空间

    saveDC.SelectObject(saveBitMap)  # 高度saveDC，将截图保存到saveBitmap中

    # 选择合适的 window number，如0，1，2，3，直到截图从黑色变为正常画面
    result = windll.user32.PrintWindow(hwnd, saveDC.GetSafeHdc(), 3)

    bmpinfo = saveBitMap.GetInfo()
    bmpstr = saveBitMap.GetBitmapBits(True)

    img = Image.frombuffer(
        'RGB',
        (bmpinfo['bmWidth'], bmpinfo['bmHeight']),
        bmpstr, 'raw', 'BGRX', 0, 1)

    win32gui.DeleteObject(saveBitMap.GetHandle())
    saveDC.DeleteDC()
    mfcDC.DeleteDC()
    win32gui.ReleaseDC(hwnd, hwndDC)
    print(6)
    if result == 1:
        # cropped_img = img.crop((0, 0, 100, 100))
        # 保存图像
        # cropped_img.save("screenshot.png")
        img.show()
        # im.save("test.png")  # 调试时可打开，不保存图片可节省大量时间（约0.2s）
        # return im  # 返回图片
    else:
        print("fail")


photo_capture()
# 获取要截图的窗口句柄（这里假设是记事本窗口）

# hwnd = 198260
# if hwnd:
#     photo_capture(hwnd)
# else:
#     print("未找到指定窗口")