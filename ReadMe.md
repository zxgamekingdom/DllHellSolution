# Dll地狱

当一个可执行程序引用A与B两个库时,但是A与B库都引用了一个名C的库,但A与B引用的C版本不一致,并且C的向后兼容性不足,那么程序就会出现DLL地狱.

# 解决方法

## 处理`AppDomain`的`AssemblyResolve`的事件

处理`AppDomain`的`AssemblyResolve`的事件,这个事件最好在Main函数最开始的时候订阅处理..NET会在运行到对应类的逻辑时就会加载对应类所在的dll.

`public event ResolveEventHandler? AssemblyResolve;`

`public delegate System.Reflection.Assembly? ResolveEventHandler(object? sender, ResolveEventArgs args);`

例如:可执行程序引用了A与B库,A与B库引用了不同版本的C库,如果不删除可执行程序同目录下的C.dll,则A与B则会使用这个C.dll,无论该C.dll的版本为多少,并且不会触发AppDomain的AssemblyResolve的事件.如果可执行程序也需要引用C.dll,则可以创建一个新的类库项目,将依赖C.dll的逻辑写入到这个类库中.

## 使用GAC(还未测试)

GAC将不同版本的dll添加进GAC中,.NET会自动去GAC加载对应版本的dll.

只有经过强命名的dll才能添加进GAC中.

## 使用ILMerge或者ILRepack(还未测试)

使用这两个工具将dll及其依赖的所有dll从IL层面打包成一个dll.

例如:Unity就可以使用这个技术,因为Unity使用了Newtonsoft.Json,而其他也有很多类库使用了这个库,导致Unity会疯狂报错.

Fody的合并的插件只是将dll所有依赖的dll以嵌入式资源的形式打包进了dll中,然后订阅处理了`AppDomain`的`AssemblyResolve`的事件,处理的逻辑就是当需要解析程序集的时候,就读取以前打包进dll的嵌入式资源,将其解析为程序集,最后将解析出来的程序集作为事件订阅处理函数的返回值返回.所以有处理`AppDomain`的`AssemblyResolve`的事件一样的限制所在.
