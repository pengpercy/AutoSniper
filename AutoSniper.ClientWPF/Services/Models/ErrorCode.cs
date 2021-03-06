﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSniper.ClientWPF.Services.Models
{
    public enum ErrorCode
    {
        调用成功 = 1000,
        一般错误提示 = 1001,
        内部错误 = 1002,
        验证不通过 = 1003,
        资金安全密码锁定 = 1004,
        资金安全密码错误 = 1005,
        实名认证等待审核或审核不通过 = 1006,
        此接口维护中 = 1009,
        人民币账户余额不足 = 2001,
        比特币账户余额不足 = 2002,
        莱特币账户余额不足 = 2003,
        以太币账户余额不足 = 2005,
        ETC币账户余额不足 = 2006,
        BTS币账户余额不足 = 2007,
        EOS币账户余额不足 = 2008,
        账户余额不足 = 2009,
        挂单没有找到 = 3001,
        无效的金额 = 3002,
        无效的数量 = 3003,
        用户不存在 = 3004,
        无效的参数 = 3005,
        无效的IP或与绑定的IP不一致 = 3006,
        请求时间已失效 = 3007,
        交易记录没有找到 = 3008,
        API接口被锁定或未启用 = 4001,
        请求过于频繁 = 4002,
    }
}
