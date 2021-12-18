# GrapeCity Documents PDF Viewer

#### [日本語](#japanese)

A full-featured JavaScript PDF viewer and editor that comes with [GrapeCity Documents for PDF](https://www.grapecity.com/documents-api-pdf).

__GrapeCity Documents PDF Viewer__ (__GcDocs PDF Viewer__, __GcPdfViewer__) is a fast, modern JavaScript based PDF viewer and editor that runs in all major browsers.
The viewer can be used as a cross platform solution to view (or modify - see _Support API_ below) PDF documents on Windows, MAC, Linux, iOS and Android devices.
GcPdfViewer is included in [GrapeCity Documents for PDF (GcPdf)](https://www.grapecity.com/documents-api-pdf) - a feature-rich cross-platform PDF API library for .NET Core.

[_Support API_](#support_api) is a server-side ASP.&#8203;NET Core library that ships as C# source code, and allows you to easily set up a server
that uses GcPdf to provide PDF modification features to GcPdfViewer. When connected to _Support API_,
GcPdfViewer can be used as a PDF editor to save filled PDF forms, remove sensitive content, edit annotations and forms, and more.

See __docs/index.html__ for GcPdfViewer client API documentation.

Product highlights:

- Works in all modern browsers, including IE11, Edge, Chrome, FireFox, Opera, Safari
- When connected to _GcPdf_ on the server via _Support API_, provides:
  - Customizable and mobile-friendly form filler
  - Real-time collaboration mode
  - Annotation and form editors
  - PDF redaction
  - Signature verification
  - Other editing features
- Works with frameworks such as React, Preact, Angular
- Supports form filling; filled forms can be printed or form data can be submitted to the server
- Provides caret for text selection/copy, including vertical and RTL texts
- Includes thumbnails, text search, outline, attachments, articles and layers panels
- Allows opening PDF files from local disks
- Supports annotations including text, free text, rich text etc.
- Supports redact annotations (including appearance streams), allows user to hide or show redacts
- Supports sound annotations
- Allows rotating and printing the rotated document
- Supports article threads navigation
- Fully supports file attachments (both attachment annotations and document level attachments)
- Comes with several themes, new custom themes can be added
- Supports external CMaps
- ...and more.

## [2.2.19] - 05-Nov-2021
### Fixed
- Tab order is inconsistent with Acrobat Reader when using Structure tab order. (DOC-3668)

## [2.2.17] - 07-Oct-2021
### Added
- Property optionalContentConfig: the optional content (layers) configuration.
  ```javascript
  // Example: hide the optional content group (layer) with id "13R":
  const config = await viewer.optionalContentConfig;
  config.setVisibility("13R", false);
  viewer.repaint();
  ```
  ```javascript
  // Example: print information about available layers to the console:
  const config = await viewer.optionalContentConfig;
  console.table(config.getGroups());
  ```
### Fixed
- The request type for version / lastError requests from the viewer has been changed to POST. (DOC-3656)

## [2.2.16] - 29-Sep-2021
### Changed
- Minor changes for the Japanese release.

## [2.2.15] - 15-Sep-2021
### Added
- Layers panel: lists and enables users to show/hide individual PDF layers (optional content). (DOC-3539)
  ```javascript
  // Usage example:
  viewer.addLayersPanel();
  ```
- New method openPanel(): opens a side panel.
  ```javascript
  // Usage example:
  const layersPanelHandle = viewer.addLayersPanel();
  viewer.open("house-plan.pdf").then(()=> {
    viewer.openPanel(layersPanelHandle);
  });
  ```
- New method closePanel(): closes the side panel.
  ```javascript
  // Usage example:
  viewer.closePanel();
  ```
- New method resetChanges(): resets the document to its original state, discarding all changes. (DOC-3608)
  ```javascript
  // Usage example:
  await viewer.resetChanges();
  ```
- New method setPageRotation(pageIndex, rotation): enables users to rotate a specific page in the PDF. (DOC-3632)\
  This method requires SupportApi. Valid values for rotation are 0, 90, 180, and 270 degrees.
  ```javascript
  // Example: set the first page rotation to 180 degrees:
  await viewer.setPageRotation(0, 180);
  ```
- New method getPageRotation(pageIndex): gets the rotation value for a specified page.
  ```javascript    
  // Example: get the first page's rotation (degrees):
  var rotation = viewer.getPageRotation(0);
  ```
### Fixed
- When an annotation is added in code and saved, its name changes. (DOC-3619)
- The state of the Signature Tool is not cleared after recreating the GcPdfViewer component. (DOC-3621)
- Console shows an error after calling viewer.newDocument(). (DOC-3622)
- Incorrect behavior if viewer.newDocument() is called immediately after opening a PDF. (DOC-3625)

## [2.2.14] - 30-Aug-2021
### Fixed
- [Editor] Size specified by the editorDefaults.rect property is not applied to target annotation. (DOC-3597)
- If the updateLayout method is called before opening a document, the list of predefined stamps is not loaded. (DOC-3578)
- Console shows multiple error messages when the mouse is moved over a text note annotation. (DOC-3579)
- [Reply Tool] The comment icon is inconsistent with the corresponding annotation. (DOC-3580)
- [Reply Tool] Comments are not loaded when the reply tool is added after opening the document. (DOC-3581)
- The document title is not updated after setting friendlyFileName. (DOC-3577)
- gcpdfviewer.worker.js cannot be loaded from an external CDN URL. (DOC-2804)
- [Android] Viewing file properties raises the Viewer display area, but not the Viewer tool area. (DOC-3529)
- [iOS 12] [Safari] [Editor] Cannot cancel selecting image for stamp annotation. (DOC-3518, DOC-3548)
- In some cases the setPageSize method throws an error. (DOC-3304)
- When the width of the browser is less than 820px, the viewer version cannot be shown. (DOC-3526)
- [Reply Tool] Only sticky notes annotation texts show up in the comments. (DOC-3270)
- [Reply Tool] Replies to text annotations are not deleted correctly. (DOC-2409)
- [Reply Tool] Current user cannot add replies if viewer.options.replyTool.allowAddReplyOtherUser is false. (DOC-3574)
- [Annotation Editor] A sound annotation stops working after editing. (DOC-3569)
- Setting friendlyFileName does not affect the downloaded PDF name. (DOC-3575)

## [2.2.11] - 09-Aug-2021
### Notable changes:
- Added predefined stamps support to the Stamp tool. (DOC-3130)
- Allow the user to select font family for text fields and free text annotations. (DOC-3132)
- Allow the user to specify opacity for annotations. (DOC-2759, DOC-2954)
- Allow the user to specify annotations tab order. (DOC-3168)
- Multiple bug fixes.

### See __CHANGELOG.&#8203;md__ for detailed release notes.

## See it in action

- [GrapeCity Documents PDF Viewer demo site](https://www.grapecity.com/documents-api-pdfviewer/demos/)
  shows the various features of GcPdfViewer, including features that rely on [_Support API_](#support_api).
  On that site you can also modify the client side code and see the effect of the changes.
- All demos in [GrapeCity Documents for PDF Sample Browser](https://www.grapecity.com/documents-api-pdf/demos/) use GcPdfViewer to show sample PDFs.

## Installation

### To install the latest release version:

```sh
npm install @grapecity/gcpdfviewer
```

### To install from the zip archive:

Go to https://www.grapecity.com/download/documents-pdf and follow the directions on that page to get your 30-day evaluation and deployment license key.
The license key will allow you to develop and deploy your application to a test server.
Unzip the viewer distribution files (list below) to an appropriate location accessible from the web page where the viewer will live.

Viewer zip includes the following files:

- README.&#8203;md (this file)
- CHANGELOG.&#8203;md
- gcpdfviewer.js
- gcpdfviewer.worker.js
- package.json
- index.html (sample page)
- Theme files:
  - themes/dark-yellow.css
  - themes/dark-yellow.jscss
  - themes/light-blue.css
  - themes/light-blue.jscss
  - themes/viewer.css
  - themes/viewer.jscss
- Predefined CMap files for Chinese, Japanese, or Korean text output:
  - resource/bcmaps/*.bin
- TypeScript declaration files:
  - typings/*.*

## Documentation

Online documentation is available [here](https://www.grapecity.com/documents-api-pdf/docs/online/grapecitydocumentspdfviewer.html).

## Licensing

GrapeCity Documents PDF Viewer is a commercially licensed product. Please [visit this page](https://www.grapecity.com/licensing/documents-api) for details.

## Getting more help

GrapeCity Documents PDF Viewer is distributed as part of GrapeCity Documents for PDF.
You can ask any questions about the viewer, or report bugs using the
[Documents for PDF public forum](https://www.grapecity.com/forums/documents-pdf).

## More details on using the viewer

### Adding the viewer to an HTML page:

```HTML
<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <!-- Limit content scaling to ensure that the viewer zooms correctly on mobile devices: -->
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta name="theme-color" content="#000000" />
    <title>GrapeCity Documents PDF Viewer</title>
    <script type="text/javascript" src="lib/gcpdfviewer.js"></script>
    <script>
        function loadPdfViewer(selector) {
            var viewer = new GcPdfViewer(selector,
              {
                /* Specify options here */
                renderInteractiveForms: true
              });
            // Skip the 'report list' panel:
            // viewer.addReportListPanel();
            viewer.addDefaultPanels();
            // Optionally, open a PDF (will only work if this runs from a server):
            viewer.open('HelloWorld.pdf');
            // Change default viewer toolbar:
            viewer.toolbarLayout.viewer.default = ['$navigation', '$split', 'text-selection', 'pan', '$zoom', '$fullscreen',
              'save', 'print', 'rotate', 'view-mode', 'doc-title'];
            viewer.applyToolbarLayout();
        }
    </script>
  </head>
  <body onload="loadPdfViewer('#root')">
    <div id="root"></div>
  </body>
</html>
```

### How to license the viewer:

Set the GcPdfViewer Deployment key to the GcPdfViewer.License property before you create and initialize GcPdfViewer.
This must precede the code that references the js files.

```javascript
  // Add your license
  GcPdfViewer.LicenseKey = 'xxx';
  // Add your code
  const viewer = new GcPdfViewer("#viewer1", { file: 'helloworld.pdf' });
  viewer.addDefaultPanels();
```

### <a id="support_api"></a>Support API

_Support API_ is a server-side library that ships as C# source code with GcPdf. The full source code is also included
with all __GcPdfViewer__ samples that can be downloaded from the [GcPdfViewer demo site](https://www.grapecity.com/documents-api-pdfviewer/demos/).
The code can be built to use in an ASP.&#8203;NET Core or a WebForms application (see #if WEB_FORMS in the sources).
When GcPdfViewer is connected to Support API, its editing features are enabled. The edits done on the client are accumulated,
and when the user clicks 'save', the document and the edits are sent to the server, the edits are applied, and the modified PDF
is sent back to the client.

To set up a server that provides Support API in an ASP.&#8203;NET Core app (ASP.&#8203;NET Core 3.0 or later is required):

- Download any of the samples from the [GcPdfViewer demo site](https://www.grapecity.com/documents-api-pdfviewer/demos/)
  (e.g. [Edit PDF](https://www.grapecity.com/documents-api-pdfviewer/demos/edit-pdf/purejs)).
- Unpack the downloaded .zip into a directory.
- In that directory you will find a WebApi.zip file containing the full SupportApi C# sources (WebApi.zip/WebApi/SupportApi),
  plus an ASP.&#8203;NET Core (WebApi.zip/WebApi/AspNetCore.sln) and a WebForms (WebApi.zip/WebApi/WebForms.sln) sample projects.
- Running the **run.cmd** batch file in the sample directory will unpack the SupportApi files, build and start the service,
  build and run the sample and open the sample URL (http://localhost:3003) in your default browser.
- The directory WebApi.zip/WebApi/SupportApi and everything inside it is the Support API library. It can be used as is in your own projects.
- WebApi.zip/WebApi/SupportApi/Controllers/GcPdfViewerController.cs contains the API entry points. Check it out to see what operations are available.
- For more details check out the readme.&#8203;md that is included in all downloaded viewer samples.

NOTE that you will need a GrapeCity Documents for PDF Professional license to use Support API in your apps.

### Keyboard shortcuts

#### Viewer mode

- ```Ctrl +``` - zoom in
- ```Ctrl -``` - zoom out
- ```Ctrl 0``` - zoom to 100%
- ```Ctrl 9``` - zoom to window
- ```Ctrl A``` - select all
- ```R``` - rotate clockwise
- ```Shift R``` - rotate counterclockwise
- ```H``` - enable pan tool
- ```S``` - enable selection tool
- ```V``` - enable selection tool
- ```Ctrl O``` - open local PDF
- ```Ctrl F``` - text search
- ```Ctrl P``` - print
- ```Home``` - go to start of line
- ```Ctrl Home``` - go to start of document
- ```Shift Home``` - select  to start of line
- ```Shift Ctrl Home``` - select  to start of document
- ```End``` - go to end of line
- ```Ctrl End``` - go to end of document
- ```Shift End``` - select  to end of line
- ```Shift Ctrl End``` - select to end of document
- ```Left``` - go left
- ```Shift Left``` - select left
- ```Alt Left``` - go back in history
- ```Right``` - go right
- ```Shift Right``` - select right
- ```Alt Right``` - go forward in history
- ```Up``` - go up
- ```Shift Up``` - select up
- ```Down``` - go down
- ```Shift Down``` - select down
- ```PgUp``` - page up
- ```PgDown``` - page down
- ```Shift PgUp``` - select page up
- ```Shift PgDown``` - select page down

#### Editing modes

- ```Delete``` - delete selected annotation/field
- ```Esc``` - unselect annotation/field
- ```Ctrl Z``` - undo
- ```Ctrl Y``` - redo
- ```Ctrl Shift Z``` - redo

### Toolbar items

The default viewer toolbar items layout (items starting with '$' are inherited from the base viewer, other items are PDF viewer specific.):

```
['open', '$navigation', '$split', 'text-selection', 'pan', '$zoom', '$fullscreen', 'rotate', 'view-mode', 'theme-change', 'download', 'print', 'save', 'hide-annotations', 'doc-title', 'doc-properties', 'about']
```

The full list of the PDF-viewer specific toolbar items:

```
'text-selection', 'pan', 'open', 'save', 'download', 'print', 'rotate', 'theme-change', 'doc-title', 'view-mode', 'single-page', 'continuous-view'
```

The full list of base viewer toolbar items:

```
'$navigation' '$refresh', '$history', '$mousemode', '$zoom', '$fullscreen', '$split'
```

You can get default base viewer items by using the getDefaultToolbarItems() method, e.g.:

```javascript
const toolbar: Toolbar = viewer.toolbar;
let buttons = toolbar.getDefaultToolbarItems();
buttons = buttons.filter(b => b !== '$refresh');
```

To specify a custom set of toolbar items, use the toolbarLayout property and applyToolbarLayout() method, e.g.:

```javascript
viewer.toolbarLayout.viewer = {
  default: ["$navigation", 'open', '$split', 'doc-title'],
  fullscreen: ['$fullscreen', '$navigation'],
  mobile: ["$navigation", 'doc-title']
};
viewer.toolbarLayout.annotationEditor = {
  default: ['edit-select', 'save', '$split', 'edit-text'],
  fullscreen: ['$fullscreen', 'edit-select', 'save', '$split', 'edit-text'],
  mobile: ['$navigation']
};
viewer.toolbarLayout.formEditor = {
  default: ['edit-select-field', 'save', '$split', 'edit-widget-tx-field', 'edit-widget-tx-password'],
  fullscreen: ['$fullscreen', 'edit-select-field', 'save', '$split', 'edit-widget-tx-field', 'edit-widget-tx-password'],
  mobile: ['$navigation']
};
viewer.applyToolbarLayout();
```

Here is an example of how to create your own custom toolbar button:

```javascript
const toolbar: Toolbar = viewer.toolbar;
toolbar.addItem({
  key: 'my-custom-button',
  iconCssClass: 'mdi mdi-bike',
  title: 'Custom button',
  enabled: false,
  action: () => {
    alert("Custom toolbar button clicked");
  },
  onUpdate: (args) => ({ enabled: viewer.hasDocument }),
});
viewer.toolbarLayout.viewer.default =  ['$navigation', 'my-custom-button'];
viewer.applyToolbarLayout();
```

### Using the viewer in Preact

Add a reference to the viewer script.

```HTML
<body>
  <script type="text/javascript" src="/lib/gcpdfviewer/gcpdfviewer.js"></script>
  ...
```

Add the placeholder to your App template, e.g.:

```HTML
<section id="pdf"></section>
```

Render the viewer:

```javascript
let viewer = new GcPdfViewer('section#pdf');
viewer.addDefaultPanels();
```

---

# <a id="japanese"></a>GrapeCity PDF ビューワ（日本語版）

__GrapeCity PDF ビューワ__ (__GcPdfViewer__) は、主要なブラウザで動作する、JavaScriptベースのPDFビューワおよびエディタです。Windows、MAC、Linux、iOS、Androidなどのデバイス上でPDFドキュメントを表示 (または編集 - 下記の _Support API_ を参照) するためのクロスプラットフォームソリューションとして使用することができます。
なお、本PDFビューワは、クロスプラットフォーム環境にてPDFを作成・編集できるAPIライブラリ [DioDocs for PDF](https://www.grapecity.co.jp/developer/diodocs/pdf) に付属する製品となります。

[_SupportAPI_](#support_api_ja) は、C#のソースコードにて提供されるサーバーサイドのASP.&#8203;NET Coreライブラリであり、DioDocs for PDFを使用してPDFビューワにPDFの編集機能を提供するサーバを簡単に構築することができます。_Support API_ に接続すると、PDFビューワをPDFエディタとして使用し、記入済みのPDFフォームの保存、機密コンテンツの削除、注釈やフォームの編集などを行うことができます。

PDFビューワのクライアントAPIの詳細については、[こちら](https://docs.grapecity.com/help/diodocs/pdfviewer/)をご参照ください。

製品の特徴：
- IE11、Edge、Chrome、FireFox、Opera、Safariを含むすべての最新ブラウザで動作します
- _Support API_ 経由で _DioDocs for PDF_  に接続した場合、以下の機能を提供します：
  - カスタマイズ可能でモバイルフレンドリーなフォームフィラー
  - リアルタイムなコラボレーションモード
  - 注釈とフォームの編集
  - PDFコンテンツの墨消し
  - 署名の検証
  - その他の編集機能
- React、Preact、Angular などのフレームワークで動作します
- フォームの入力に対応：記入済みフォームの印刷や、フォームデータのサーバへの送信にも対応しています
- 縦書きテキストや右横書きテキストを含む、テキストの選択/コピーのためのキャレットを提供します
- サムネイル、テキスト検索、ブックマーク、添付ファイル、アーティクルのパネルが含まれます
- ローカルディスクからPDFファイルを開くことができます
- テキスト、フリーテキスト、リッチテキストなどの注釈に対応しています
- 墨消し注釈（APストリームを含む）に対応し、墨消しの表示・非表示が可能です
- 音声注釈に対応しています
- 文書の回転や、回転させた文書の印刷が可能です
- アーティクルのスレッドナビゲーションに対応しています
- 添付ファイル（添付ファイル注釈と文書レベルの添付ファイルの両方）に完全に対応しています
- 複数のテーマがすでに備わっており、さらにカスタムテーマを追加することも可能です
- 外部のCMapに対応しています
- その他、様々な機能を提供しています

## リリースノート
日本語版として動作を保証しているのは、リリースノートに記載しているバージョンのみとなります。

### [2.2.16] - 2021年9月29日
#### 主な変更点:
- スタンプツールに定義済みスタンプを追加しました。
- テキストフィールドとフリーテキスト注釈にてフォントファミリーを選択できるようになりました。
- 注釈の不透明度を指定できるようになりました。
- フォームのタブの順序を指定できるようになりました。

##### 詳細なリリースノートについては、 __CHANGELOG-JP.&#8203;md__ をご参照ください。

## 製品デモ
- [PDFビューワの製品デモ](https://demo.grapecity.com/diodocs/pdfviewer/demos/) では、[_SupportAPI_](#support_api_ja) を使用する編集機能を含め、
  PDFビューワの様々な機能を紹介しています。
  このデモでは、クライアント側のコードを変更し、どのように反映されるか確認することもできます。
- [DioDocs for PDFの製品デモ](https://demo.grapecity.com/diodocs/pdf/) 内のすべてのデモでは、PDFビューワを使用してPDFを表示しています。

## インストール
```sh
npm install @grapecity/gcpdfviewer
```

## 製品ヘルプ
製品ヘルプは[こちら](https://docs.grapecity.com/help/diodocs/pdf/#grapecitydocumentspdfviewer.html)からご覧いただけます。

## ライセンス
GrapeCity PDF ビューワのご利用にはライセンスが必要となります。詳しくはWebサイトの[ライセンス手続き](https://www.grapecity.co.jp/developer/support/license#docapi)ページをご覧ください。

## 製品情報
GrapeCity PDF ビューワは、DioDocs for PDF の一部として提供しております。製品に関する詳細な情報については、Webサイトの[製品情報](https://www.grapecity.co.jp/developer/diodocs/pdf)ページをご参照ください。

## PDFビューワの詳細な使用方法
### HTMLページへのPDFビューワの追加
```HTML
<!DOCTYPE html>
<html lang="ja">
<head>
    <meta charset="utf-8">
    <!--コンテンツの拡大縮小を制限して、モバイルデバイスでビューワが正しくズームできるようにします。-->
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta name="theme-color" content="#000000" />
    <title>GrapeCity PDF ビューワ</title>
    <script type="text/javascript" src="lib/node_modules/@grapecity/gcpdfviewer/gcpdfviewer.js"></script>
    <script>
        function loadPdfViewer(selector) {
            var viewer = new GcPdfViewer(selector,
                {
                    /* ここでオプションを指定します */
                    renderInteractiveForms: true
                });
            viewer.addDefaultPanels();
            // 必要に応じてPDFを開きます（サーバーから実行時にのみ機能）
            viewer.open('HelloWorld.pdf');
            // デフォルトのビューワのツールバーを変更します
            viewer.toolbarLayout.viewer.default = ['$navigation', '$split', 'text-selection', 'pan', '$zoom', '$fullscreen',
                'save', 'print', 'rotate', 'view-mode', 'doc-title'];
            viewer.applyToolbarLayout();
        }
    </script>
</head>
<body onload="loadPdfViewer('#root')">
    <div id="root"></div>
</body>
</html>
```

### PDFビューワへのライセンスの適用
GcPdfViewer のインスタンスを作成して初期化する前に、PDFビューワのライセンスキーを GcPdfViewer.License プロパティに設定します。
なお、これは jsファイルを参照するコードの前に記述する必要があります。

```javascript
  // ご自身のライセンスを追加してください
  GcPdfViewer.LicenseKey = 'xxx';
  // 適宜コードを追加してください
  const viewer = new GcPdfViewer('#root', { file: 'helloworld.pdf' });
  viewer.addDefaultPanels();
```

### PDFビューワの検索オプション
PDFビューワには、以下のようなテキストの検索オプションが用意されていますが、**英語のみの対応**となっており、日本語は正しく検索できない場合があります。
- 大/小文字を区別
- 単語単位で検索
- 単語の先頭を検索
- 単語の末尾を検索
- ワイルドカード
- 近接

上記検索オプションを非表示にしたい場合は、ページの`<head>`部分に以下のとおりCSSスタイルを追加してください。
```HTML
<style>
    .gc-viewer .search .search__query-params > label:nth-child(1),
    .gc-viewer .search .search__query-params > label:nth-child(2),
    .gc-viewer .search .search__query-params > label:nth-child(3),
    .gc-viewer .search .search__query-params > label:nth-child(4),
    .gc-viewer .search .search__query-params > label:nth-child(5),
    .gc-viewer .search .search__query-params > label:nth-child(6) {
        display: none;
    }
</style>
```

### <a id="support_api_ja"></a>SupportAPI
_SupportAPI_ は、DioDocs for PDFとともにC#のソースコードにて提供されるサーバーサイドのライブラリです。完全なソースコードは、[PDFビューワの製品デモ](https://demo.grapecity.com/diodocs/pdfviewer/demos/) からダウンロードできるすべてのPDFビューワのサンプルに含まれています。
このソースは、ASP.&#8203;NET CoreまたはWebFormsアプリケーションで使用するためにビルドすることができます。 （WebForms の場合は、ソースの #if WEB_FORMS をご参照ください。）
PDFビューワがSupportAPIに接続されると、編集機能が有効になります。クライアント上で行われた編集は蓄積されており、ユーザーが「保存」をクリックすると、ドキュメントと編集内容がサーバーに送信され、編集が適用されて、変更されたPDFがクライアントに返送されます。

ASP.&#8203;NET Coreアプリ（ASP.&#8203;NET Core 3.0以上が必要）でSupportAPIを提供するサーバを設定するには：

- [PDFビューワの製品デモ](https://demo.grapecity.com/diodocs/pdfviewer/demos/) から任意のサンプルをダウンロードしてください
（例：[PDFの編集](https://demo.grapecity.com/diodocs/pdfviewer/demos/edit-pdf/purejs)）。
- ダウンロードした.zipをディレクトリに解凍します。
- そのディレクトリには「WebApi.zip」が格納されており、完全なSupportAPIのC#ソース（WebApi.zip/WebApi/SupportApi）、
ASP.&#8203;NET Core（WebApi.zip/WebApi/AspNetCore.sln）及びWebForms（WebApi.zip/WebApi/WebForms.sln）のサンプルプロジェクトが含まれています。
- サンプルのディレクトリにある**run.cmd**バッチファイルを実行すると、SupportApiのファイルが解凍され、サービスがビルドされて起動されるとともに、
サンプルもビルドされて実行され、サンプルのURL (http://localhost:3003) がデフォルトのブラウザで開きます。
- ディレクトリ「WebApi.zip/WebApi/SupportApi」とその中にあるものは、SupportAPIライブラリです。お客様のプロジェクトでそのままお使いいただけます。
- 「WebApi.zip/WebApi/SupportApi/Controllers/GcPdfViewerController.cs」には、APIのエントリーポイントが含まれています。どのような操作が可能か確認してみてください。
- 詳細については、ダウンロードしたビューワのサンプルに含まれているreadme.&#8203;mdをご覧ください。

なお、SupportAPIを使用するには、有効なDioDocs for PDFライセンスが必要です。
ライセンスは、GcPdfDocumentのSetLicenseKey静的メソッドにて設定します。以下の例は、「SupportApi/Controllers/GcPdfViewerController.cs」にコンストラクタを追加し、その中でDioDocs for PDFライセンスをSupportAPIに適用する方法を示しています。
```C#
public GcPdfViewerController()
    {
        GcPdfDocument.SetLicenseKey("ライセンスキー");
    }
```

### キーボードのショートカット
#### 表示モード
- ```Ctrl +``` - 拡大
- ```Ctrl -``` - 縮小
- ```Ctrl 0``` - 100％に拡大
- ```Ctrl 9``` - ページ幅に合わせて拡大
- ```Ctrl A``` - すべて選択
- ```R``` - ドキュメントを右回りに回転
- ```Shift R``` - ドキュメントを左回りに回転
- ```H``` - 手のひらツールを有効化
- ```S``` - 選択ツールを有効化
- ```Ctrl O``` - PDFファイルを開く
- ```Ctrl F``` - テキスト検索
- ```Ctrl P``` - 印刷
- ```Home``` - 行頭に移動
- ```Ctrl Home``` - ドキュメントの先頭に移動
- ```Shift Home``` - 行頭まで選択
- ```Shift Ctrl Home``` - ドキュメントの先頭まで選択
- ```End``` - 行末に移動
- ```Ctrl End``` - ドキュメントの末尾に移動
- ```Shift End``` - 行末まで選択
- ```Shift Ctrl End``` - ドキュメントの末尾まで選択
- ```Left``` - 左に移動
- ```Shift Left``` - 左を選択
- ```Alt Left``` - 前の履歴に戻る
- ```Right``` - 右に移動
- ```Shift Right``` - 右を選択
- ```Alt Right``` - 次の履歴に進む
- ```Up``` - 上に移動
- ```Shift Up``` - 上まで選択
- ```Down``` - 下に移動
- ```Shift Down``` - 下まで選択
- ```PgUp``` - 前のページに移動
- ```PgDown``` - 次のページに移動
- ```Shift PgUp``` - 前のページまで選択
- ```Shift PgDown``` - 次のページまで選択

#### 編集モード
- ```Delete``` - 選択した注釈/フィールドを削除
- ```Esc``` - 注釈/フィールドの選択を解除
- ```Ctrl Z``` - 元に戻す
- ```Ctrl Y``` - やり直す
- ```Ctrl Shift Z``` - やり直す

### ツールバーの項目
PDFビューワのデフォルトのツールバーレイアウトは以下のとおりです。（'$'で始まる項目は、ビューワの標準として備わっているものであり、それ以外の項目はPDFビューワ固有のものになります。）
```
['open', '$navigation', '$split', 'text-selection', 'pan', '$zoom', '$fullscreen', 'rotate', 'view-mode', 'theme-change', 'download', 'print', 'save', 'hide-annotations', 'doc-title', 'doc-properties', 'about']
```

PDFビューワ固有のツールバー項目は以下のとおりです。
```
'text-selection', 'pan', 'open', 'save', 'download', 'print', 'rotate', 'theme-change', 'doc-title', 'view-mode', 'single-page', 'continuous-view'
```

ビューワの標準のツールバー項目は以下のとおりです。
```
'$navigation' '$refresh', '$history', '$mousemode', '$zoom', '$fullscreen', '$split'
```

以下のように、getDefaultToolbarItems() メソッドを使用することで、デフォルトのツールバー項目を取得することができます。
```javascript
const toolbar: Toolbar = viewer.toolbar;
let buttons = toolbar.getDefaultToolbarItems();
buttons = buttons.filter(b => b !== '$refresh');
```

ツールバー項目をカスタマイズするには、以下のように toolbarLayout プロパティと applyToolbarLayout() メソッドを使用します。
```javascript
viewer.toolbarLayout.viewer = {
  default: ["$navigation", 'open', '$split', 'doc-title'],
  fullscreen: ['$fullscreen', '$navigation'],
  mobile: ["$navigation", 'doc-title']
};
viewer.toolbarLayout.annotationEditor = {
  default: ['edit-select', 'save', '$split', 'edit-text'],
  fullscreen: ['$fullscreen', 'edit-select', 'save', '$split', 'edit-text'],
  mobile: ['$navigation']
};
viewer.toolbarLayout.formEditor = {
  default: ['edit-select-field', 'save', '$split', 'edit-widget-tx-field', 'edit-widget-tx-password'],
  fullscreen: ['$fullscreen', 'edit-select-field', 'save', '$split', 'edit-widget-tx-field', 'edit-widget-tx-password'],
  mobile: ['$navigation']
};
viewer.applyToolbarLayout();
```

以下のように、独自のカスタムツールバーボタンを作成することもできます。
```javascript
const toolbar: Toolbar = viewer.toolbar;
toolbar.addItem({
  key: 'my-custom-button',
  iconCssClass: 'mdi mdi-bike',
  title: 'カスタムボタン',
  enabled: false,
  action: () => {
    alert("カスタムツールバーボタンがクリックされました");
  },
  onUpdate: (args) => ({ enabled: viewer.hasDocument }),
});
viewer.toolbarLayout.viewer.default =  ['$navigation', 'my-custom-button'];
viewer.applyToolbarLayout();
```

### Preact でのPDFビューワの使用
PDFビューワのスクリプトへの参照を追加します。
```HTML
<body>
  <script type="text/javascript" src="/lib/gcpdfviewer/gcpdfviewer.js"></script>
  ...
```

アプリのテンプレートに以下のようにプレースホルダを追加します。
```HTML
<section id="pdf"></section>
```

PDFビューワをレンダリングします。
```javascript
let viewer = new GcPdfViewer('section#pdf');
viewer.addDefaultPanels();
```

---
_The End._
