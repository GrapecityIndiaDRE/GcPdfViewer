﻿﻿﻿# 更新履歴（日本語版）
このファイルには、GrapeCity PDF ビューワ（日本語版）のすべての更新履歴が記載されています。
なお、日本語版として動作を保証しているのは、本ファイルに記載のあるバージョンのみとなります。

更新履歴のフォーマットは、[「Keep a Changelog」](https://keepachangelog.com/ja/1.0.0/)を参考にしています。また、GrapeCity PDF ビューワのプロジェクトは、[「Semantic Versioning」](https://semver.org/lang/ja/spec/v2.0.0.html)に準拠しています。

## [2.2.16] - 2021年9月29日
### 追加
- 定義済みスタンプが使用できるようになりました。\
  新しい stamp オプションを使用して設定を行うことができます。
  ```javascript
  // 例１：「承認」と「否認」のキャプションが付いたカスタムスタンプ２セットをスタンプ注釈のドロップダウンに追加します。
  var viewer = new GcPdfViewer("#root", { 
    stamp: {
      stampCategories: [
        { name: '承認', stampImageUrls: ['http://example.com/stamps/ok.png', 
           'http://example.com/stamps/agree.png', 'http://example.com/stamps/fine.png'] },
        { name: '否認', stampImageUrls: ['http://example.com/stamps/stamps/notok.png', 
          'http://example.com/stamps/disagree.png', 'http://example.com/stamps/noway.png'] },
      ]
    }
  });
  ```
  ```javascript
  // 例２：定義済みスタンプのドロップダウンを非表示にします。
  var viewer = new GcPdfViewer("#root", {     
    stamp: {
      stampCategories: false
    }
  });
  ```
  ```javascript
  // 例３：カスタムスタンプの画像解像度を指定します（指定がない場合は 72 dpi が使用されます）。
  var viewer = new GcPdfViewer("#root", { 
    stamp: {
        dpi: 144,
      stampCategories: [
        { name: 'スタンプ', stampImageUrls: ['stamp1.png', 'stamp2.png', 'stamp3.png'] }
      ]
    }
  });
  ```
- disableFeatures オプションを追加しました。\
  このオプションにて、特定の機能を無効にすることができます（例：セキュリティを考慮して）。\
  無効にできる機能：\
  'JavaScript' | 'AllAttachments' | 'FileAttachments' | 'SoundAttachments' | 'DragAndDrop' | 'SubmitForm' | 'Print'
  ```javascript
  // 例：ドラッグアンドドロップ操作、JavaScript アクション、すべての添付ファイルを無効にします。
  var viewer = new GcPdfViewer("#root", { disableFeatures: ['DragAndDrop', 'JavaScript', 'AllAttachments'] } );
  ```
- [エディタ] 注釈の不透明度に対応しました。
- [エディタ] テキストフィールドとフリーテキスト注釈にフォントファミリーのサポートを追加しました。
- [エディタ] 署名ツールにより作成されたスタンプ注釈を自動的にコンテンツに変換する機能を追加しました。
  ```javascript
  // 使用例：
  var viewer = new GcPdfViewer("#root", signTool: { convertToContent: true });  
  ```
- [エディタ] setPageSize メソッドを追加しました。\
  これにより、newPage メソッドにて独自のページサイズを指定できるようになりました。
  
  ```javascript
  // 例：1ページ目の新しいページサイズを設定します。
  viewer.setPageSize(0, { width: 300, height: 500 } );
  ```
- [フォームエディタ] ページ内のタブの順序を変更する機能を追加しました。
- 注釈の行と列のタブの順序のサポートを追加しました。
- マス目テキストフィールドに必須のバリデーションを実装しました。
- ドキュメント一覧パネルにて現在のドキュメントがハイライト表示されるようになりました。

### 変更
- [エディタ] 選択した注釈／フォームにカーソルを合わせた際のカーソルのスタイルを改善しました。
- [エディタ] いずれかのエディタツールが使用される際、フローティングバーが非表示になるよう変更しました。
- 使用される SupportApi のバージョンが古い場合、警告メッセージが表示されるようになりました。

### 不具合の修正
- [[4404354875535](https://developer-tools.zendesk.com/hc/ja/articles/4404354875535)] [PDFビューワ] PushButtonFieldに設定されたActionURIが正しく動作しない
- [[4404354881295](https://developer-tools.zendesk.com/hc/ja/articles/4404354881295)] [PDFビューワ] 日本語のテキストを含むJavaScriptアクションが正しく動作しない

## [2.1.17] - 2021年5月26日
### 追加
- [iOS] iOS デバイスでファイル選択ダイアログを開くための UI を追加しました。
- [ビューワ] PDF ファイルをドラッグ & ドロップすることで開けるようになりました。
- [エディタ] グラフィカルな署名ツールを追加しました。
- [エディタ] スタンプ注釈に対応しました。（画像をスタンプ注釈として追加できます。また、画像のコンテンツへの変換も可能です。）
- [エディタ] locked プロパティを使用して、編集の際に注釈やフォームフィールドをロックする機能を追加しました。
  ```javascript
  // 使用例:
  var viewer = new GcPdfViewer('#root', { supportApi: { apiUrl: 'api/pdf-viewer', webSocketUrl: false } });
  viewer.addDefaultPanels();
  viewer.addAnnotationEditorPanel();
  viewer.addFormEditorPanel();
  viewer.addReplyTool();
  viewer.onAfterOpen.register(()=>{
    // ドキュメントを開いた後、すべてのテキスト注釈をロックします。
    const resultArr = await viewer.findAnnotation(1, // 1 - AnnotationTypeCode.TEXT
      { findField: 'annotationType',
        pageNumberConstraint: 1, findAll: true });
      viewer.updateAnnotations(0, resultArr.map((data)=> { data.annotation.locked = true; return data.annotation; }));
  });
  // Annotations.pdf を開きます。
  viewer.open('Annotations.pdf');
  ```
- [エディタ] コンテキストメニューを使用して、オブジェクトを次のページまたは前のページに移動できるようになりました。
- [iOS] [Android] スマートフォンやタブレットでの PDF フォームの入力に対応しました。
- [エディタ] リンク注釈に対応しました。
  * 選択したテキストのコンテキストメニューを使用して、リンク注釈を作成できるようになりました。 
  * コンテキストメニューを使用して、注釈やフォームフィールドの上にリンク注釈を作成できるようになりました。
~~~~
  「移動先の表示方法」の説明:
    FitV = FitBV    // ページ高に合わせる
    FitH = FitBH    // ページ幅に合わせる
    Fit = FitB      // ページに合わせる
    FitR            // 設定した矩形に合わせて移動しズーム
    XYZ             // 設定した座標に移動し倍率を適用
~~~~
- [エディタ] Shift キーを使用して、アスペクト比を維持しながら注釈やフォームフィールドのサイズを変更できるようになりました。
- PDF ビューワのメソッドが PDF の JS アクションにて使用できるようになりました。
```javascript
  // 署名ダイアログを表示する JS アクションの例です。
  app.showSignTool();
```
- 新しいメソッドを追加しました。
```javascript
  lockAnnotation    // 編集の際に注釈をロックします。
  unlockAnnotation  // 編集の際に注釈のロックを解除します。
  getPageSize       // ページサイズを返します。
                    // デフォルトではスケーリングされていないサイズを返しますが、
                    // スケーリングされた値を取得したい場合は、
                    // includeScale 引数に true を渡します。
  getPageRotation   // 閲覧時のページの回転値を取得します。
```
- 新しいオプション coordinatesPrecision を追加しました。
```javascript
  // 注釈やフォームフィールドの配置座標の精度を設定できます。
  // 注釈エディタとフォームエディタに設定が反映されます。 
  // デフォルトは 1（小数点以下は四捨五入）です。
  // 使用例:
  // デフォルトの精度を 0.001（端数は四捨五入）に変更します。
  var viewer = new GcPdfViewer("#root", { coordinatesPrecision: 0.001 } );
```
- [エディタ] サイズ変更 / 移動ハンドルのデフォルトサイズを変更できるようになりました。
  editorDefaults オプションを使用して resizeHandleSize と moveHandleSize の設定を調整します。
  デフォルト値は resizeHandleSize が 8 ピクセルで、moveHandleSize が 14 ピクセルです。
```javascript
  // サンプルコード:
  var viewer = new GcPdfViewer("#root", {
       editorDefaults: {
       resizeHandleSize: 20,
       moveHandleSize: 40,
       dotHandleSize = 20
   },
   supportApi: { apiUrl: 'support-api/gc-pdf-viewer', webSocketUrl: false }
 });
```
- 「手のひらツール」と「テキスト選択ツール」を含むフローティングバーを追加しました。
  フローティングバーは、編集モードではデフォルトで表示されます。
  editorDefaults.hideFloatingBar の設定により、フローティングバーを非表示にできます。
```javascript
  // サンプルコード:
  var viewer = new GcPdfViewer("#root", {
     editorDefaults: {
         hideFloatingBar: true
     },
     supportApi: { apiUrl: 'support-api/gc-pdf-viewer', webSocketUrl: false }
   });
```
- SupportApi が利用可能な場合、「GrapeCity PDF ビューワ について」のダイアログに SupportApi のバージョンが表示されるようになりました。
- 同一グループ内のラジオボタンの出力値を自動生成する機能を実装しました。
- プッシュボタンにて、MouseUp / MouseDown イベントのアクションに対応しました。
- validateForm メソッドを使用して、カスタムバリデーションを実行できるようになりました。
```javascript
  // 使用例:
  // すべてのフォームフィールドを検証します。
  // 各フィールドの値は「YES」または「NO」である必要があります。
  viewer.validateForm((fieldValue, field) => { return (fieldValue === "YES" || fieldValue === "NO") ? true : "入力可能な値は YES または NO です。"; });
```
- 新しい API メソッド setAnnotationBounds が追加されました。注釈の位置やサイズをプログラムにて設定できます。
 ```javascript
 // 例: 注釈を左上に移動します。
 viewer.setAnnotationBounds('1R', {x: 0, y: 0});
 // 例: 注釈を左下に移動します。
 viewer.setAnnotationBounds('1R', {x: 0, y: 0}, 'BottomLeft');
 // 例: 注釈のサイズを 40 x 40 ポイントに設定します。
 viewer.setAnnotationBounds('1R', {w: 40, h: 40});
 // 例: 注釈の位置を x = 50、y = 50（原点は左上）、サイズを 40 x 40 ポイントに設定します。
 viewer.setAnnotationBounds('1R', {x: 50, y: 50, w: 40, h: 40});
 ```
- 行と列の注釈タブの順序に対応しました。
- マス目フィールドに必須入力のバリデーションを設定できるようになりました。 

### 変更
- [Android/Chrome] 下にスワイプするとページが更新されるようになりました。
- [iOS] iOSデバイスのデフォルトのズームモードを「ページ幅に合わせる」に変更しました。
- 検証に失敗または空の必須フィールドがあるフォームは送信できなくなりました。代わりに、最初に失敗したフィールドがフォーカスされ、エラーを示すツールチップとともに強調表示されます。
- パスワード入力ダイアログのスタイルを改善しました。
- repaint メソッドにオプションの indicesToRepaint 引数を指定できるようになりました。
```javascript
// 使用例:
// インデックスが 0 と 3 のページのコンテンツと注釈を再描画します。
viewer.repaint([0, 3]);
```

## [2.0.17] - 2021年2月17日
### 追加
- 日本語版としての初版となります。
