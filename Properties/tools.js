// 默认sendKey，可在启动监听时修改
// 在Winfrom中添加下一行代码
// let currentSendKey = "";

// 配置信息
const CONFIG = {
  // 通知标题和内容
  notifications: {
    timeOut: {
      title: "星宝农场时长耗完通知",
      desp: "检测到腾讯先锋云游戏时长已耗尽警告，星宝农场已掉线，请立即查看",
    },
    loginFailed: {
      title: "腾讯先锋账号过期通知",
      desp: "检测到腾讯先锋已掉线，请立即查看",
    },
    // kasi: {
    //   title: "游戏界面卡死通知",
    //   desp: "检测到游戏界面已卡死，请立即查看",
    // },
  },
};

// SCT160433Tnc7nQn8EYFpmQJot4VcRe9KL
// SCT276074Txh7ohix5QmpmXX98lzkqQOSK

/**
 * server酱通知
 * @param {string} title 通知标题
 * @param {string} desp 通知内容
 * @param {string} [sendKey] 可选，覆盖默认的sendKey
 */
function sendServerChan(title, desp, sendKey) {
  const keyToUse = sendKey || currentSendKey;
  if (!keyToUse) {
    console.error("Server酱通知失败: 未提供SendKey");
    return;
  }

  const apiUrl =
    "https://sctapi.ftqq.com/" +
    keyToUse +
    ".send?title=" +
    title +
    "&desp=" +
    desp;

  fetch(apiUrl)
    .then((response) => response.json())
    .then((result) => {
      if (result.code === 0) {
        console.log("推送成功:", result);
      } else {
        console.error("推送失败:", result);
      }
    })
    .catch((error) => console.error("请求异常:", error));
}
/**
 * 调用 WinForms 函数
 * @param {string} text - 要发送的消息
 */
function callWinForms(text, color = "black") {
  if (window.chrome?.webview?.postMessage) {
    const messageObj = {
      text: text,
      color: color,
    };
    window.chrome.webview.postMessage(JSON.stringify(messageObj));
  } else {
    console.log("WebMessage 不可用");
  }
}

/**
 * 自动处理游戏无操作和自动退出情况
 */
function initGameAutoHandlers() {
  // 需要监控的元素及其处理方式
  const handlers = [
    {
      selector: "div.el-loading-mask is-fullscreen",
      action: (element) => {
        element.remove();
        callWinForms("REFRESH");
        console.log("检测游戏重连，已刷新页面");
        callWinForms("检测游戏重连，已刷新页面");
      },
    },
    {
      selector: "div.el-message-box__wrapper",
      action: (element) => {
        element.remove();
        console.log("检测到遮挡，已删除");
      },
    },
    {
      selector: "div.pop",
      action: (element) => {
        element.remove();
        console.log("检测到遮挡，已删除");
      },
    },
    {
      selector: "div.close-btn",
      action: (element) => {
        element.click();
        console.log("检测到有元素遮挡，点击关闭");
      },
    },
    {
      // 关闭辅助屏
      selector: ".g-pc-s-v-close",
      action: (element) => {
        element.click();
        console.log("检测到有元素遮挡，点击关闭");
      },
    },
    {
      selector:
        "#xf-body-tag > div:nth-child(13) > div > div:nth-child(3) > div:nth-child(3)",
      action: (element) => {
        element.click();
        console.log("检测到无操作提示，已自动点击继续游戏");
      },
    },
    {
      selector: "#game-tipbg",
      action: () => {
        callWinForms("检测到游戏自动退出，执行自动刷新页面");
        callWinForms("REFRESH");
      },
    },
    {
      selector: "#el-loading-text",
      action: () => {
        callWinForms("REFRESH");
        callWinForms("检测游戏重连，已刷新页面");
      },
    },
  ];

  // 检查并执行对应操作
  function checkAndHandleElements() {
    handlers.forEach((handler) => {
      const element = document.querySelector(handler.selector);
      if (element) handler.action(element);
    });
  }

  // 立即检查一次
  checkAndHandleElements();

  // 设置DOM变化监听
  const observer = new MutationObserver(checkAndHandleElements);
  observer.observe(document.body, {
    childList: true,
    subtree: true,
  });

  // 返回observer以便需要时停止监听
  return observer;
}
// 初始化监听
const gameObserver = initGameAutoHandlers();
// 如需停止监听，可调用：gameObserver.disconnect();

/**
 * 防抖函数
 * 在指定的时间间隔内，将多次触发的事件合并为一次执行。
 * @param {Function} func 要进行防抖处理的函数。
 * @param {number} delay 延迟执行的毫秒数。
 * @returns {Function} 返回一个新的防抖函数。
 */
function debounce(func, delay) {
  let timeoutId;
  return function (...args) {
    clearTimeout(timeoutId);
    timeoutId = setTimeout(() => {
      func.apply(this, args);
    }, delay);
  };
}

/**
 * 通用控制台监听器
 * @param {'log' | 'warn'} method - 要监听的方法
 * @param {string | Function} matcher - 文本或自定义匹配函数
 * @param {Function} callback - 匹配时的回调
 */
function listenConsole(method, matcher, callback) {
  const original = console[method];
  console[method] = function (...args) {
    original.apply(console, args);

    // 自定义匹配函数
    if (typeof matcher === "function") {
      if (matcher(args)) return callback(args);
    }
    // 文本匹配逻辑
    else {
      for (const arg of args) {
        // 检查对象中的 message 属性
        if (arg?.message?.includes?.(matcher)) {
          return callback(arg);
        }
        // 检查字符串
        if (typeof arg === "string" && arg.includes(matcher)) {
          return callback(arg);
        }
      }
      // 检查拼接后的字符串
      if (args.join(" ").includes(matcher)) {
        callback(args);
      }
    }
  };

  // 返回恢复函数
  return () => {
    console[method] = original;
    return `已停止监听${method}`;
  };
}

// 监听有摇杆进入省电模式，自动跳跃
const powerSavingMode = listenConsole(
  "log",
  "11999",
  debounce((arg) => {
    // console.warn("检测到进入省电模式：" + arg);
    callWinForms("检测到进入省电模式");
    Mouse.click(btnOffset.jumpBtn);
  }, 300) // 300毫秒内只执行一次
);

// 监听游戏卡死
const stuck = listenConsole("log", "ReconnectError", (arg) => {
  // console.warn("检测到游戏页面卡死：" + arg);
  callWinForms("检测到游戏页面卡死");
  // 调用 WinForms 函数：刷新
  callWinForms("REFRESH");
});

// 监听游戏卡死
const webSocketClosed = listenConsole("error", "WebSocket", (arg) => {
  // console.warn("检测到游戏页面卡死：" + arg);
  callWinForms("检测到WebSocket已处于关闭状态");
  // 调用 WinForms 函数：刷新
  callWinForms("REFRESH");
});

// 监听云游戏时长已耗尽警告
const timeoutListener = listenConsole("warn", "耗尽", (arg) => {
  // console.log("检测到云游戏时长已耗尽警告:", arg);
  callWinForms("检测到云游戏时长已耗尽警告");
  if (typeof currentSendKey !== "undefined") {
    sendServerChan(
      CONFIG.notifications.timeOut.title,
      CONFIG.notifications.timeOut.desp
    );
  }
});

// 监听登录信息过期
const loginListener = listenConsole("warn", "请登录", (arg) => {
  // console.log("检测到登录失效:", arg);
  callWinForms("检测到登录失效");
  if (typeof currentSendKey !== "undefined") {
    sendServerChan(
      CONFIG.notifications.loginFailed.title,
      CONFIG.notifications.loginFailed.desp
    );
  }
});
