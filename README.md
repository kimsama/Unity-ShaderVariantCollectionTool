# Unity-ShaderVariantCollectionTool
A simple shader variant collection tool for Unity 5.x

Unity's default behaviour is to postpone the compilation / optimization of a shader loaded at runtime until it appears on screen.

See [ShaderVariantCollection.WarmUp](https://docs.unity3d.com/ScriptReference/ShaderVariantCollection.WarmUp.html) document for more details. 

It collects shader variants which will be used during a play session and save it out as an asset so which can be set on 'Graphics Setting', 
so it allows preloading that shaders which can prevent shader compilation problem causes cpu spike when a model is firstly shown.

## References

* [Shader variant at `Dragonjoon`'s blog page](http://dragonjoon.blogspot.kr/2015/08/variants.html) - Well described about Unity's shader variants. (*written in Korean*)
* [Shader variant at `Es_Program` on Qiita](http://qiita.com/Es_Program/items/79edf9f8fca786b365aa) - Well described about conditional compilation of Unity shader and its *`keywords`*.(*written in Japanges*)
* [Using Shader Variant at `vui` on Qiita]() - *`AddShaderVariantsWindow`* was found on here. (*written in Japanges*)