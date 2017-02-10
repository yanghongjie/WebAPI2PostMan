# WebAPI2PostMan

- WebAPI的项目使用Nuget安装
- Install-Package WebAPI2PostMan
- 安装完毕后，打开当前项目地址比如为http://localhost，在后面输入/PostMan，http://localhost/PostMan 即可打开可用于导入PostMan格式的数据
- 打开PostMan -> Import -> Import From Link -> 输入http://localhost/PostMan -> 点击Import 即可
- 默认导入的包名会是WebAPI2PostMan，假设需要修改为 MyTestAPI 只需要将导入地址更改为http://localhost/PostMan?serviceName=MyTestAPI
