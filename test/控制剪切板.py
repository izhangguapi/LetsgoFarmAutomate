import win32clipboard as w
import win32con

# 打开剪切板
w.OpenClipboard(None)
# 清空剪切板
w.EmptyClipboard()
# 需要复制的文本
text = "回去睡觉"
# 将文本转换为字节形式
text_bytes = text.encode("gbk")
# 将文本放到剪切板上
w.SetClipboardData(win32con.CF_TEXT, text_bytes)
# 关闭剪切板
w.CloseClipboard()
