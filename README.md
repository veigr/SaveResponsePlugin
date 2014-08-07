SaveResponsePlugin
--

KanColleViewer で ResponseBody を保存するプラグインです。

### 機能

* リソースデータや、APIのJSON風Responseデータをファイルに保存します。
    * KanColleViewer ディレクトリ直下に `ResponseData` ディレクトリを作成し、保存します。
    * ファイルは常に上書きされます。
    * 既にIEのキャッシュにあるファイルは保存されません。

### インストール

* `SaveResponsePlugin.dll` を KanColleViewer の `Plugins` ディレクトリに放り込んで下さい。

### 使用ライブラリ

* [KanColleViewer](http://grabacr.net/kancolleviewer)
* [Reactive Extensions](http://rx.codeplex.com/)
* [Livet](http://ugaya40.net/livet)
* [FiddlerCore](http://fiddler2.com/fiddlercore)


#### ライセンス

* MIT License
