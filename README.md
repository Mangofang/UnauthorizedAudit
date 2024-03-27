# UnauthorizedAudit
**此项目可在代码审计过程中帮助对未授权代码进行定位和审计**

**通过判断WebRequest的返回值实现 如返回结果是登录页或进行进行跳转则判断不存在未授权**

**需要注意 判断WebRequest的返回值意味着需要在本地搭建网站或存在远程网站**

![e27451f602493c8ce8e4fc427de83a28](https://github.com/Mangofang/UnauthorizedAudit/assets/38810849/89318a91-cc4e-4734-aa7d-8ae3deaa16b3)

**自用项目 简单加了个UI 未过多测试 如有问题或需求请提交Issues**

**如果你不知道怎么做，不推荐使用该程序**

## 声明：
1. 文中所涉及的技术、思路和工具仅供以安全为目的的学习交流使用，任何人不得将其用于非法用途以及盈利等目的，否则后果自行承担！

## 使用指南

1. 按照需要的参数填写代码路径、URL、返回值过滤
2. 选择后缀类型
3. 开始扫描

## 返回值过滤规则
**程序使用Contains进行字符匹配，并非正则过滤，填入此处的值为需要软件过滤掉的存在授权的返回信息**

**例如：** HTTP 404 （过滤掉返回值存在“HTTP 404”的结果）

**通常返回的信息可能有多个，如404或302跳转，又或者不同程序语言或框架的报错信息，此时可以使用“&”符号分割关键词**

**例如：** HTTP 404&<script>parent.location.href='/admin/login.aspx'</script> （此处将过滤404的结果以及通过script跳转login.aspx的结果）

## 更新
2024年03月27日
  1.修复除aspx外其他后缀不可用的情况
  2.修复了debug问题

2024年03月24日
  1. 仓库公开
  
![e27451f602493c8ce8e4fc427de83a28](https://github.com/Mangofang/UnauthorizedAudit/assets/38810849/89318a91-cc4e-4734-aa7d-8ae3deaa16b3)
