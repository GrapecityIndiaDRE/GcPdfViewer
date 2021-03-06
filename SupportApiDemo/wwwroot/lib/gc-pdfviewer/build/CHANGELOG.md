# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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
### Added
- Added support for predefined stamps. (DOC-3130)\
  Use the new stamp option to configure the settings.
  ```javascript
  // Example 1: add two sets of custom stamps with captions 'Okay' and 'Not okay' to the stamps drop-down:
  var viewer = new GcPdfViewer("#root", { 
    stamp: {
      stampCategories: [
        { name: 'Okay', stampImageUrls: ['http://example.com/stamps/ok.png', 
           'http://example.com/stamps/agree.png', 'http://example.com/stamps/fine.png'] },
        { name: 'Not okay', stampImageUrls: ['http://example.com/stamps/stamps/notok.png', 
          'http://example.com/stamps/disagree.png', 'http://example.com/stamps/noway.png'] },
      ]
    }
  });
  ```
  ```javascript
  // Example 2: hide the predefined stamps drop-down:
  var viewer = new GcPdfViewer("#root", {     
    stamp: {
      stampCategories: false
    }
  });
  ```
  ```javascript
  // Example 3: specify image resolution for custom stamps (if unspecified, 72dpi is used):
  var viewer = new GcPdfViewer("#root", { 
    stamp: {
        dpi: 144,
      stampCategories: [
        { name: 'Stamps', stampImageUrls: ['stamp1.png', 'stamp2.png', 'stamp3.png'] }
      ]
    }
  });
  ```
- Added disableFeatures option. (DOC-3298)\
  This option allows disabling certain features (e.g. due to security considerations).
  Features that can be disabled:\
  'JavaScript' | 'AllAttachments' | 'FileAttachments' | 'SoundAttachments' | 'DragAndDrop' | 'SubmitForm' | 'Print'.
  ```javascript
  // Example: disable DragAndDrop operations, JavaScript actions, all attachments:
  var viewer = new GcPdfViewer("#root", { disableFeatures: ['DragAndDrop', 'JavaScript', 'AllAttachments'] } );
  ```
- [Editor] Support opacity for annotations. (DOC-2954)
- [Editor] Added font family support for text fields and free text annotations. (DOC-3132)
- [Editor] Added ability to automatically convert the Signature Tool's stamp to content. (DOC-3152)
  ```javascript
  // Usage example:
  var viewer = new GcPdfViewer("#root", signTool: { convertToContent: true });  
  ```
- [Editor] Added method setPageSize: allows specifying custom page size for the newPage method. (DOC-2751)
  ```javascript
  // Example: set new page size for the first page:
  viewer.setPageSize(0, { width: 300, height: 500 } );
  ```
- [Form Editor] Added ability to modify a page's tab order. (DOC-3168)
- Added support for Row and Column annotations tab order. (DOC-2797)
- Implemented required validation for comb fields. (DOC-2739)
### Changed
- Show a warning message if the connected SupportApi version is out of date.
- [Editor] Improved selection box cursor styles. (DOC-3146)
- [Editor] The floating toolbar is hidden when any editor tool is activated.
### Fixed
- Multiple bug fixes.

## [2.1.21] - 5-Jul-2021
### Fixed
- [Editor] The 'hide annotation' button does not hide annotations. (DOC-3277)
- Combobox dropdown is hidden behind the next page. (DOC-3300)
- Incorrect display of annotations in a rotated PDF. (DOC-3303)
- [Editor] The editor collapses when clicking a thread bead annotation. 
  (Editing thread bead annotations is not supported yet, so they should not be listed by the editor at all.) (DOC-3352)
- [Form Editor] Saving the PDF after changing the fields' tab order produces corrupted PDF. (DOC-3294)
- [Form Editor] Cannot focus on fileds after setting tab order. (DOC-3292)
- [Editor] Cannot save the PDF after modifying tab order. (DOC-3295)
- [Editor] Font name is missing after saving and reloading a PDF. (DOC-3279)
- [Editor] Inconsistent behavior after setting annotation opacity. (DOC-3264, DOC-3265)
- [Editor] No default font name for free text annotations and text fields. (DOC-3273)
- [Editor] The 'hide annotation' button does not hide annotations. (DOC-3277)
- Combobox dropdown is hidden behind the next page. (DOC-3300)
- Incorrect display of annotations in a rotated PDF. (DOC-3303)
### Changed
- [Redact annotations] 'Opacity' property label has been changed to 'Fill Opacity'. (DOC-3266)

## [2.1.18] - 09-Jun-2021
### Added
- Highlight the current document in the Document List panel. (DOC-3243)
### Fixed
- Incorrect display of some annotation positions when the annotation size is small. (DOC-3203)
- Incorrect display of comb-text fields. (DOC-3199)
- Signature field's popup still shows when editor mode has changed to view mode. (DOC-1777)
- [Documentation] GcPdfSearcher class is missing. (DOC-3205)
- PolyLine annotations are not hidden when the "Hide annotations/form fields" tool is checked. (DOC-3272)
- The viewer does not initialized correctly when the viewer script is loaded after the DOM has been loaded. (DOC-3283)
- [Editor] The attached image disappears after the undo operation. (DOC-3204)
- [Editor] The appearance of comb text field has a slight glitch after setting back color. (DOC-3194)
- [Editor] Popup does not appear for audio annotation after editing. (DOC-3193)
- [Editor] It doesn't focus on the new added textNote if adding a new textNote annotation with Context Menu.	(DOC-3247)
- [Editor] The textNote annotation is not selected after I set any status. (DOC-3246)
- [Editor] The sound annotation exceeds the boundary of the resize handle. (DOC-3195)
- [Editor] Circle Annotations are not resizing on the third page in GcPdfViewer. (DOC-3250)
- PolyLine annotations are not hidden when the "Hide annotations/form fields" is checked. (DOC-3272)
- Viewer is not initialized correctly when the viewer script is loaded after the DOM has been loaded. (DOC-3283)
- [Editor] Fixed multiple issues when editing annotations. (DOC-3204, DOC-3194, DOC-3193, DOC-3247, DOC-3246, DOC-3195, DOC-3250)

## [2.1.17] - 20-May-2021
### Fixed
- The position of resize handle is incorrect for redact annotation. (DOC-3122)
- The default icon of file attachment annotation is not displayed. (DOC-3118)
- Incorrect X conversion for XYZ destination. (DOC-3137)
- Text position of FreeText annotation is incorrect after saving. (DOC-3141)
- The status icon of a TextNote annotation is not displayed. (DOC-3113)
- The graphical signature stamp disappears after printing. (DOC-3160)
- [Editor] Border style in editorDefaults is ignored. (DOC-3176)
- [Editor] Incorrect display when resizing items. (DOC-3183)
- [Editor] Errors while reordering fields or annotations. (DOC-3155)
- [Editor] Unable to select other annotations after adding new PolyLine annotation. (DOC-3145)
- [Android] The menu does not pop up on a long button press. (DOC-3071)
- [Localization] "Paperclip" icon name is not localized. (DOC-3163)
### Added
- Added support for Row and Column annotation tab order. (DOC-2797)
- Implemented required validation for comb fields. (DOC-2739)

## [2.1.14] - 23-Apr-2021
### Added
- [iOS] Provided an additional UI that can be used to open the file selection dialog on iOS device. (DOC-2878)
- [Viewer] Added the ability to open PDF files with Drag and Drop operation.
- [Editor] Added graphical signature tool. (DOC-2270)
- [Editor] Added stamp annotation support (allows adding images as stamp annotations; images can be converted to content). (DOC-2612)
- [Editor] Added the ability to lock annotations and fields for editing using the 'locked' property. (DOC-2642)
  ```javascript
  // Example:
  var viewer = new GcPdfViewer('#root', { supportApi: { apiUrl: 'api/pdf-viewer', webSocketUrl: false } });
  viewer.addDefaultPanels();
  viewer.addAnnotationEditorPanel();
  viewer.addFormEditorPanel();
  viewer.addReplyTool();
  viewer.onAfterOpen.register(()=>{
    // Lock all text annotations after document open:
    const resultArr = await viewer.findAnnotation(1, // 1 - AnnotationTypeCode.TEXT
      { findField: 'annotationType',
        pageNumberConstraint: 1, findAll: true });
      viewer.updateAnnotations(0, resultArr.map((data)=> { data.annotation.locked = true; return data.annotation; }));
  });
  // Open Annotations.pdf
  viewer.open('Annotations.pdf');
  ```
- [Editor] Added the ability to move objects to next or previous page using the context menu.
- [iOS] [Android] Support for filling PDF forms on phone or tablet. (DOC-2867)
- Link annotations support (DOC-1847):
  * added the ability to create a link annotation using the context menu for the selected text context. 
  * added the ability to create a link annotation over any other annotation using the context menu.
~~~~
  Explicit destination types description:
    FitV = FitBV    // fit page height
    FitH = FitBH    // fit page width
    Fit = FitB      // fit page
    FitR            // Scroll and zoom rectangle into view.
    XYZ             // Scroll to X, Y and apply Zoom
~~~~
- [Editor] Added the ability to use the Shift key to maintain the aspect ratio of the annotation box during resize.
- JS Actions: GcPdfViewer's methods are now available for JS actions in the PDF.
```javascript
  // An example of a JS action to show the signature dialog:
  app.showSignTool();
```
- Added new methods:
```javascript
  lockAnnotation    // Lock annotation for editing.
  unlockAnnotation  // Unlock annotation for editing.
  getPageSize       // Returns the page size. By default returns unscaled size, 
                    // pass true for the includeScale argument if you want to get the scaled value.
  getPageRotation   // Get page view box rotation value.
```
- Added new option: coordinatesPrecision.
```javascript
  // Annotation coordinates rounding precision. 
  // Used by Annotation and Form editors. 
  // Default is 1 (round fraction part).
  // Usage example:
  // Change the default rounding precision to 0.001:
  var viewer = new GcPdfViewer("#root", { coordinatesPrecision: 0.001 } );
```
- [Editor] Added the ability to change the default size for resize/move selection box handles. (DOC-2888)
  Use the editorDefaults options to tune resizeHandleSize and moveHandleSize settings.
  Default value is 8 pixels for resizeHandleSize, and 14 pixels for moveHandleSize.
```javascript
  // Sample code:
  var viewer = new GcPdfViewer("#root", {
       editorDefaults: {
       resizeHandleSize: 20,
       moveHandleSize: 40,
       dotHandleSize = 20
   },
   supportApi: { apiUrl: 'support-api/gc-pdf-viewer', webSocketUrl: false }
 });
```
- Added an additional floating bar that contains Pan and Text selection tools.
  The Floating bar is visible by default in editor mode.
  You can hide the floating bar with the editorDefaults.hideFloatingBar setting.
```javascript
  // Sample code:
  var viewer = new GcPdfViewer("#root", {
     editorDefaults: {
         hideFloatingBar: true
     },
     supportApi: { apiUrl: 'support-api/gc-pdf-viewer', webSocketUrl: false }
   });
```
- The About dialog box now displays the SupportApi version when SupportApi is available.
- Implemented auto-generated export value for a radio button in the same radio group. (DOC-2742)
- Push buttons: added support for MouseUp/MouseDown event actions. (DOC-2826)
- Added the ability to perform custom validation using the validateForm method. (DOC-2256)
```javascript
  // Usage example:
  // Validate all form fields, each field must have the value "YES" or "NO".
  viewer.validateForm((fieldValue, field) => { return (fieldValue === "YES" || fieldValue === "NO") ? true : "Possible value is YES or NO."; });
```
### Changed
- [Android/Chrome] Swipe down does not refresh the page when finger is over the scrolling area.
- [iOS] Default zoom mode for iOS devices changed to PageWidth. (DOC-2938)
### Fixed
- [iOS] [Android] Fixed multiple issues that occurred on mobile devices.
- Miscellaneous bug fixes.
- Inline text editor cannot be activated by double-clicking. (DOC-2982)
  * Limitation: inline text editing is not supported on iOS.

## [2.0.24] - 20-Mar-2021
### Fixed
- [JS Action, regression] app.popUpMenu() is not working correctly. (DOC-2903)
- [Form Filler] The form filler button is disabled in some cases. (DOC-2823)
- [Form Filler] The form filler does not work if the value of a radio field is set to null. (DOC-2824)
- Issues with filling PDF forms on touch devices. (DOC-2867)
- [Safari] Text field margins are too wide. (DOC-2897)

## [2.0.21] - 04-Mar-2021
### Fixed
- Default theme colors show momentarily before the specified theme is applied. (DOC-2810)
- Screen blinking when adding / removing or updating annotation. (DOC-2785)
- JS Action: isBoxChecked returns an incorrect value in some cases. (DOC-2803)
- [Editor] Some annotation property values are lost after saving and reloading a PDF. (DOC-2806, DOC-2809)
### Changed
- A form that fails validation or has empty required fields now cannot be submitted.
  Instead, the first failed field is focused and highlighted, with a tooltip indicating the error. (DOC-2753)
- Improved the styles of the password dialog box.
### Added
- New API method setAnnotationBounds: programmatically set the position and/or the size of an annotation.
 ```javascript
 // Example: move an annotation to the top left corner:
 viewer.setAnnotationBounds('1R', {x: 0, y: 0});
 // Example: move an annotation to the bottom left corner:
 viewer.setAnnotationBounds('1R', {x: 0, y: 0}, 'BottomLeft');
 // Example: set an annotation size to 40 x 40 points:
 viewer.setAnnotationBounds('1R', {w: 40, h: 40});
 // Example: set an annotation position to x=50, y=50 (origin top/left) and size to 40 x 40 points:
 viewer.setAnnotationBounds('1R', {x: 50, y: 50, w: 40, h: 40});
 ```

### Changed
- The repaint method now accepts optional indicesToRepaint argument.
  Usage example:
```javascript
// Redraw content and annotations for pages with indexes 0 and 3:
viewer.repaint([0, 3]);
```

## [2.0.18] - 22-Feb-2021
### Fixed
- [Editor] On applying a redact annotation its bounds are displayed incorrectly. (DOC-1976)
- [Editor] File size of a file attachment is still shown after the file has been removed. (DOC-2784)

## [2.0.17] - 10-Feb-2021
### Fixed
- [Editor] Invalid selection rectangle if a page contains x / y offsets. (DOC-2767)
- [Editor] The confirmation dialog does not appear even though the active document has been modified. (DOC-2744)
- [Editor] Clicking the cancel button in item properties does not cancel the changes. (DOC-2743)
- [Editor] Incorrect download PDF file name if the file has been modified. (DOC-2690)

## [2.0.15] - 25-Jan-2021
### Fixed
- Cannot check a radio button if appearance state name contains spaces. (DOC-2721)
- Compatibility problems when using GcPdfViewer with Wijmo controls. (DOC-2670)
- Autofocus does not work in FireFox. (DOC-2696)
- Incorrect download PDF file name. (DOC-2690)
- After navigating to an article thread, another thread cannot be activated by clicking on the thread bead. (DOC-2701)
- [SupportApi] In some scenarios submitting a filled form causes out of memory error. (DOC-2718)
- [Search] Issues when using word, wildcard and proximity options. (DOC-2682, DOC-2689, DOC-2722)
- [Editor] Incorrect display of read-only CombText fields. (DOC-2733)
- [Editor] Incorrect callout line endings. (DOC-2723)
- [Editor] Author and subject are not saved for FreeText annotations. (DOC-2686)
- [Editor] Callout line is not adjusted when a FreeText annotation is copied to another location. (DOC-2710)
- [Editor] An error occurs when setting a FreeText annotation's BackColor via API. (DOC-2707)
- [Editor] No default border is shown for newly added FreeText annotations. (DOC-2702)
- [Editor] Tooltip is not shown when hovering the mouse over an annotation after editing. (DOC-2703)
- [Editor] Error occurs when copying a Polygon/Polyline. (DOC-2715)

## [2.0.13] - 07-Jan-2021
### Fixed
- [Editor] User name is set to StickyNotes annotations only. (DOC-2632)
- [Search] Search results are incorrect if the search text contains different styles. (DOC-2578)
- Copying rich text and paste to notepad, there will be a space. (DOC-2678)

## [2.0.12] - 31-Dec-2020
### Fixed
- In some cases text copied to clipboard is incorrect. (DOC-2678)
- Invalid request to viewer.css file. (DOC-2668)
- Error when disposing the viewer. (DOC-2630)
- [Editor] User name is set to StickyNotes annotations only. (DOC-2632)
- [Editor] The result of viewer.save() is incorrect. (DOC-2629)
- [Search] Search results for "Starts with" option are incorrect in some cases. (DOC-2579)
- [Search] Search results are missing when searching across lines. (DOC-2566)
- [Search] Search results are incorrect if both "Starts with" and "Ends with" options are set. (DOC-2549)
- [Search] Wildcard search using the $ character in the query is incorrect. (DOC-2568)
- [Search] Search results using the "Whole word" option are incorrect. (DOC-2577)
- [Search] Search results using "Proximity" and "Whole word" options together are incorrect. (DOC-2554, DOC-2578)

## [2.0.11] - 09-Dec-2020
### Fixed
- Error when drawing a circle annotation. (DOC-2564)
- Validation messages are not displayed in some cases. (DOC-2518)
- The shared author indicator disappears after clicking the delete button. (DOC-2547)
- Viewer display issues (DOC-2544, DOC-2590)
- Text selection persists when a new file is loaded into the viewer. (DOC-2601)
- Errors if the viewer is removed from DOM and added back. (DOC-2587)
- The searched results are incorrect. (DOC-2565)
- [Form Filler] Form filler is not disabled after loading a PDF with fields that have been converted to content. (DOC-2558)
- [Annotation Editor] Errors when a page has two instances of GcPdfViewer. (DOC-2496)
- [Annotation Editor] Japanese text cannot be entered correctly in annotation comment. (DOC-2413)
- Localization issues. (DOC-2607)
### Added
- Japanese localization.

## [2.0.10] - 22-Nov-2020
### Fixed
- Highlighted search results are incorrect in some cases. (DOC-2562)
- [Collaboration] The viewer collapses on connection error. (DOC-2571)
### Added
- Added the ability to configure automatic reconnect interval.
  Use the reconnectInterval setting to configure automatic reconnect interval in milliseconds.
  The default reconnect interval is 5000 milliseconds (5 seconds).
  ```javascript
  // Example - reconnect after a 1 second timeout:
  var viewer = new GcPdfViewer('#root', { supportApi: 'api/pdf-viewer', reconnectInterval: 1000 } );
  // Example - disable auto reconnect:
  var viewer = new GcPdfViewer('#root', { supportApi: 'api/pdf-viewer', reconnectInterval: -1 } );
  ```
- Added the ability to configure logging level used for persistent connections.
  ```javascript
  // Example - turn on debug logging for persistent connections:
  var viewer = new GcPdfViewer('#root', { supportApi: 'api/pdf-viewer' }, logLevel: 'Debug' );
  ```

## [2.0.9] - 17-Nov-2020
### Fixed
- [IE11] Fixed regular expression exception. (DOC-2371)
- In some cases the editor is disabled on first launch. (DOC-2516)
- Unable to scroll and edit the form on mobile devices. (DOC-2502)
- The display of search results on the document is out of position in some cases. (DOC-2398)
- Double-click selects the space at the beginning of a word. (DOC-2487)
- Double-click selects punctuation characters along with letters. (DOC-2488)
- Enabling editing features on localhost incorrectly requires a Pro license. (DOC-2440)

## [2.0.3] - 05-Nov-2020
### Added
- [Annotation Editor] Added enhanced editorDefaults option that allows setting the default values and other settings of annotations. (DOC-2458)
```javascript
  // Example:
  var viewer = new GcPdfViewer("#root", {
       editorDefaults: {
           squareAnnotation: {
               borderStyle: { width: 5, style: 1 },
               color: [0, 0, 0],
               interiorColor: [255, 0, 0],
           }
       },
       supportApi: "api/pdf-viewer"
  });
```

## [2.0.2] - 04-Nov-2020
### Fixed
- [Collaboration] The share button does not work. (DOC-2399)
- [Collaboration] Previous access information is displayed for new documents. (DOC-2402)
- [Collaboration] Access permission is not changed in real time co-authoring. (DOC-2401)
- [Collaboration] Inserting a blank page is not reflected in real time co-authoring. (DOC-2410)
- [Collaboration] Focus is missing after creating a FreeText annotation. (DOC-2415)
- [Collaboration] The modifier user name disappears. (DOC-2424)
- [Form Filler] Crash when clicking FormFiller after loading a pdf. (DOC-2430)
- [Form Filler] Multiple property does not work for email type. (DOC-2435)
- [Form Filler] A TextField with search type is not displayed if the maxLength property is less than the minLength property. (DOC-2431)

### Added
- Support additional wildcards in license keys. (DOC-2347)
- Support for v2 licenses. Only v2 Pro licenses allow the use of SupportApi except on localhost. (DOC-2440)
- [Collaboration] Added new sharing option setting: presenceMode.
```javascript
  // Determines the type of presence for collaboration users. Possible values are:
  // - 'on' or true: the presence of all users, including the active one, will be shown.
  // - 'others': all users except the active user will be shown.
  // - 'off' or false: presence will not be shown.
  // Default value is 'on'.
  // Example - turn off presence indicators:
  var viewer = new GcPdfViewer("#root", {
       sharing: {
           presenceMode: 'off'
       },
       supportApi: "api/pdf-viewer"
  });
```
- [Collaboration] Added new sharing option setting: presenceColors.
```javascript
   // If specified, these colors will be used as color for presence indicators. 
   // Dictionary, key - user name, value - color string.
   // Usage example:
   var viewer = new GcPdfViewer("#root", {
        sharing: {
            knownUserNames: ['Jamie Smith', 'Lisa'],
            presenceColors: { 'Anonymous': '#999999', 'Jamie Smith': 'red',  'Lisa': 'blue' }
        },
        supportApi: "api/pdf-viewer"
   });
```
- [Form Filler] showFormFiller method, usage example:
```javascript
  if (viewer.hasForm) {
    viewer.showFormFiller();
  }
```

## [2.0.1] - 20-Oct-2020
### Fixed
- [Form Filler] The validationmessage property does not work for a date field. (DOC-2356)
- [Form Filler] Wrong tooltip position for OneColumn layout. (DOC-2386)
- [Form Filler] Incorrect order of custom form fields. (DOC-2384)
- [Form Filler] Required ignored if validateoninput is set in code. (DOC-2358)
- [Collaboration] The share button exists even if it is not configured. (DOC-2377)
- [Annotation Editor] Focus is lost when filling an annotation subject field. (DOC-2389)
- [Annotation Editor] Snap to page edge does not work when the page does not already have annotations or fields. (DOC-2394)
- Predefined annotation appearance rendering improved. (DOC-2395)

### Added
- [Form Filler] Added applyAfterFailedValidation option.
  The type of action to execute if form validation fails after clicking Apply button.
  Possible values are: 'confirm' | 'reject' | 'apply' or Function, default value is 'confirm',
  ```javascript
  // Examples:
  // Reject applying changes if validation failed:
  options.formFiller = {
    applyAfterFailedValidation: 'reject'
  }
  // Apply changes even if validation failed:
  options.formFiller = {
    applyAfterFailedValidation: 'apply'
  }
  // Execute custom function and reject changes:
  options.formFiller = {
    applyAfterFailedValidation: function() { 
      alert('Validation failed, changes rejected.'); 
      return false; 
    }
  }
  // Execute custom function and accept changes:
  options.formFiller = {
    applyAfterFailedValidation: function() { 
      alert('Validation failed, but changes are accepted.'); 
      return true; 
    }
  }
  ```

## [2.0.0] - 16-Oct-2020
### Fixed
- [Form Filler] Content cannot be cleared if setting default value for "password" type text field. (DOC-2365)
- [Form Filler] Fixed problem with checkbox and radio buttons checked state.
- [Form Filler] The beforeFieldChange event handler was not called for radio / checkbox inputs.
- [Form Filler] SpellCheck property does not work for text type. (DOC-2367)
- [Form Filler] min and max properties do not work for number type. (DOC-2361)
- Ink annotation renders incorrectly on a rotated page. (DOC-2340)
- Redact works incorrectly on a rotated page. (DOC-2337)
- The size and font of the set placeholder is different from the default placeholder. (DOC-2362)
- There is no differences in appearance between disabled and readonly for email text field. (DOC-2363)
- The checked state of the radio button follows the checked state of the check box. (DOC-2352)
- Input type date does not work in IE11. (DOC-2371)
- Setting an incorrect default value is applied successfully. (DOC-2357)

### Added
- [Form Filler] Added title option: specifies the Form Filler window title. Example:
```javascript
  options.formFiller = {
    title: 'Please fill the form'
  }
```
- [Form Filler] One column layout for Form Filler. (DOC-2375)
- Added layout option: specifies the Form Filler dialog layout.  
- Possible values are: 'Auto', 'OneColumn', 'TwoColumns'. The default is 'Auto', which uses 'TwoColumns' for large screens and 'OneColumn' for small ones.
```javascript
  // Example:
  options.formFiller = {
    layout: 'OneColumn'
  }
```
- [Form Filler] Added support for multi-line text fields (see multiline property). (DOC-2366)
- [Form Filler] Added new formFiller event handlers:
- onInitialize: called after the list of fields is loaded and initialized but not yet rendered.
- beforeApplyChanges: called when the Apply button has been clicked and fields validation was successful.
```javascript
  // Example:
  options.formFiller = {
    onInitialize: function(formFiller) { },
    beforeApplyChanges: function(formFiller) { }
  }
```
- [Form Filler] Added the ability to display custom HTML content for in the field list.
```javascript
  // Example:
  options.formFiller = {
    mappings: {
      'CustomContent_Info1': { 
        type: 'custom-content', 
        content: '<span>Some HTML content</span>'
      },
    }
  }
```
- [Form Filler] Added the ability to display a full-width field input control without a label. (DOC-1594)
- Ability to resize line annotation. (DOC-2364)
- Added page transformations support to Annotation/Form editors. (DOC-2310)
- Support additional wildcards for license keys. (DOC-2347)

## [1.3.2] - 10-Sep-2020
### Added
- Form filler: added support for min/max/inputmode properties. (DOC-1594)
### Changed
- formFiller mapping settings enhanced.
- formFiller.mapping renamed to formFiller.mappings, see docs/index.html#formfieldmapping for the full specification. (DOC-2311)

## [1.3.1] - 28-Sep-2020
### Added
- Added Form Filler feature. (DOC-1594)
- Added Collaboration Mode feature. (DOC-1595)
- Added the ability to convert annotations and fields to content. (DOC-1883)

## [1.2.97] - 04-Nov-2020
### Fixed
- Support additional wildcards in license keys. (DOC-2347)
- [Annotation Editor] Editor tools are disabled when opening a page with the viewer that is initially in edit mode. (DOC-2446)

## [1.2.96] - 28-Oct-2020
### Fixed
- Line annotation disappeared after setting author and clicking subject field. (DOC-2434)
- If the maximum length of a text field is 0, it is ignored (behavior similar to that of Acrobat Reader). (DOC-2436)
### Added
- [SupportApi] Support for token-based authentication. (DOC-2432)
- [Search] Ability to use the "Whole word" search option together with the "Wildcard" search option. (DOC-2397)

## [1.2.95] - 26-Oct-2020
### Fixed
- The position of redact is incorrect in some cases. (DOC-2405)
- The dashed style for line annotation is incorrect. (DOC-2406)
- The line start and end properties of line annotations do not work. (DOC-2407)
- The number of replies is incorrect. (DOC-2422)
- A checked checkbox cannot be unchecked. (DOC-2420)
- The appearance of read-only checkBox is incorrect. (DOC-2418)
- Incorrect behavior of annotations in rotated documents. (DOC-2393)
- Wildcard search results are incorrect in some cases. (DOC-2397)
- Japanese search is incorrect with some options. (DOC-2403)
- Text annotation comments automatically change when user tries to enter text. (DOC-2411)
- Reply comments to text annotations are not deleted correctly. (DOC-2409)
- Text selection with mouse in a scrolled document is broken (regression added in .93). (DOC-2425)

## [1.2.94] - 20-Oct-2020
### Fixed
- [Annotation Editor] Focus is lost when filling an annotation subject field. (DOC-2389)
- [Annotation Editor] Snap to page edge does not work when the page does not already have annotations or fields. (DOC-2394)
- Predefined annotation appearance rendering improved. (DOC-2395)

## [1.2.93] - 17-Oct-2020
### Fixed
- Ink annotation renders incorrectly on a rotated page. (DOC-2340)
- Redact works incorrectly on a rotated page. (DOC-2337)
### Added
- Ability to resize line annotation. (DOC-2364)
- Added page transformations support to Annotation/Form editors. (DOC-2310)



## [1.2.92] - 04-Sep-2020
### Fixed
- Memory leak occurs when clicking a document in the document list. (DOC-2293)
- Inaccurate display of the predefined appearance of a radio button if the radio button contains a border. (DOC-2303)

## [1.2.91] - 02-Sep-2020
### Fixed
- Fixed regression issue with executing JS actions for checkbox widgets with predefined appearance streams. (DOC-2088)
- The reset action does not work for password fields. (DOC-2273)
- GcPdfViewer.LicenseKey is not accessible in Angular projects. (DOC-2276)
- The "Backcolor" label is incorrect in Japanese locale. (DOC-2275)

## [1.2.89] - 06-Aug-2020
### Fixed
- Dashed border style for annotation does not work. (DOC-2255)
- Checkboxes rendered incorrectly in some cases. (DOC-2198)
- Building node_modules folder throws errors. (DOC-2232)

### Changed
- Updated client API documentation.

### Added
- Added the ability to add Ink and Redact annotation tools to the default viewer toolbar.
```javascript
  // Usage example:
  var viewer = new GcPdfViewer("#root", { supportApi: 'api/pdf-viewer' });
  viewer.toolbarLayout = { viewer: { default: ["open", 'edit-redact', 'edit-redact-apply', 'edit-ink']} };
  viewer.applyToolbarLayout();
  viewer.open("Annotations.pdf");
```
- Added support for RadiosInUnison flag. (DOC-2205)
- Added support for radio button appearance streams. (DOC-2208)
- Added replyTool settings option. (DOC-2245)
```javascript
  // The replyTool option spec:
  replyTool?: {
    readOnly?: boolean,               /* Default is false */
    allowAddNote?: boolean,           /* Default is true */
    allowChangeUserName?: boolean,    /* Default is true */
    allowAddReply?: boolean,          /* Default is true */
    allowDelete?: boolean,            /* Default is true */
    allowStatus?: boolean,            /* Default is true */
    allowChangeOtherUser?: boolean,   /* Default is true */
    allowDeleteOtherUser?: boolean,   /* Default is true */
    allowStatusOtherUser?: boolean,   /* Default is true */
    allowAddReplyOtherUser?: boolean, /* Default is true */
  }
  // Usage examples:
  //
  // Prevent changing or deleting another user's comments:
  var viewer = new GcPdfViewer('#root',
    { replyTool: {  allowChangeOtherUser: false, allowDeleteOtherUser: false },
      userName: 'John',
      supportApi: 'api/pdf-viewer' });
  viewer.addReplyTool('expanded');
  //
  // Add reply tool in read-only mode:
  var viewer = new GcPdfViewer('#root', {
    replyTool: { readOnly: true },
    userName: 'John',
    supportApi: 'api/pdf-viewer' });
  viewer.addReplyTool('expanded');
  //
  // Add reply tool that does not allow changing the author name or other users' comments:
  var viewer = new GcPdfViewer('#root', { 
    replyTool: { allowChangeUserName: false, allowChangeOtherUser: false },
    userName: 'John', 
    supportApi: 'api/pdf-viewer' });
  viewer.addReplyTool('expanded');
  //
  // Hide the "Add Sticky Note" item from the context menu:
  var viewer = new GcPdfViewer('#root', {
    replyTool: { allowAddNote: false },
    userName: 'John',
    supportApi: 'api/pdf-viewer' });
  viewer.addReplyTool();
```

## [1.2.88] - 17-July-2020
### Fixed
- The context menu closed if onBeforeCloseContextMenu tried to prevent it from closing, for example:
```javascript
   viewer.options.onBeforeCloseContextMenu = function(e) {
     console.log("The context menu should not close.");
     return false;
   };
```
- Fixed uncaught TypeError when option "renderer" is set to "svg", for example:
```javascript
  var viewer = new GcPdfViewer("#root", { renderer: "svg" } );
```
### Added
- Client API documentation, see "docs" folder.

## [1.2.87] - 15-July-2020
### Fixed
- [IE11] Regex syntax error exception. (DOC-2211)

## [1.2.86] - 14-July-2020
### Fixed
- The page jumps when adding a sound annotation. (DOC-2197)
- The position is changed after moving an ink annotation. (DOC-2196)

## [1.2.85] - 13-July-2020
### Changed
- The snapAlignment option specification changed, the updated specification:
```javascript
  // The Snap Alignment feature customization settings.
  // The *tolerance* setting is the distance between the edges of two objects within which the object that is being
  // moved or resized snaps to the other object.
  // The *margin* setting is the distance from the target object or page edge to which the edge of the object being moved or resized snaps.
  // The *center* setting allows the user to snap objects to centers of other objects (in addition to edges).
  // By default, snap tolerance is 5pt, snap margin is 10pt, snap to center is true.
  snapAlignment: true | false |
  {
    tolerance: number | { horizontal: number | false, vertical: number | false },        
    margin: false | true | number | { horizontal: number | boolean, vertical: number | boolean },
    center: false | true | { horizontal: boolean, vertical: boolean },
  }
```

## [1.2.84] - 10-July-2020
### Fixed
- The FreeText border color is not applied until back color is not selected. (DOC-2179)
### Changed
- Free text annotation: property Color renamed to Backcolor; property Border color replaced by Forecolor. (DOC-2179)

## [1.2.83] - 07-July-2020
### Fixed
- The color for searched result is green sometimes, but sometimes is black. (DOC-2175)
- Reply behavior correction: when we respond to the selected comment in the response tool, 
  the "In reply to" field now contains the selected comment as a reference (instead of the topmost comment). (DOC-2174)
- The last reply comment is put at the top. (DOC-2174)

## [1.2.82] - 06-July-2020
### Fixed
- Field under applied redact annotation can be focused. (DOC-1999)
- The state of the comb-text field value is not saved. (DOC-2150)
- The position of cursor is incorrect in comb text field. (DOC-2144)
- Non-Unicode chars in license names not supported. (GCL2-259)
- Checkboxes do not execute additional JavaScript actions. (DOC-2086)
- snapAlignment does not accept value of true. (DOC-2098)
- Setting value for fields which have same names doesn't work. (DOC-2085)
- The activity indicator does not go away if an error occurs when opening a document. (DOC-2109)
- The center alignment isn't displayed if setting snapAlignment: { tolerance: 25 }. (DOC-2114)
- Horizontal Center cannot be displayed when setting snapAlignment: { center: { vertical: true } }. (DOC-2115)
- There's no Horizontal margin if not specified it. (DOC-2116)
- Cannot link to the correct position if clicking the searched result. (DOC-2135)
- The first search result is selected after checking "highlight all" item. (DOC-2136)
- Appearance is incorrect when a radio button is checked until a blank space is clicked. (DOC-2083)
- Modifying "In reply to" to a Child comment, the comment will be disappeared from the comment panel. (DOC-2139)
- Cannot and sticky note when the Form Editor is opened. (DOC-2137)
- Cannot search correct result when the target is in text field. (DOC-2140)
### Changed
- Status labels in the annotation popup now display icons instead of text.
### Added
- Added ability to specify boolean value for snap tolerance and disable vertical or horizontal snap.
- Comb-text field: added alignment property.
- Field widgets: added support for JavaScript actions from additional-actions dictionary. (DOC-2088)

## [1.2.81] - 22-June-2020
### Fixed
- Wildcard Search does not consider newlines. (DOC-2075)
- Appearance is incorrect when you check a radio button until you click on a blank. (DOC-2083)
- Data input in a PDF form shows incorrect result. (DOC-2082)
### Added
- Search panel: highlight all feature. (DOC-2069)
- Added zoomMode property. Accepted values are: 0 - Value, 1 - PageWidth, 2 - WholePage.
```javascript
  // usage example: 
  viewer.zoomMode = 2; // Set zoom mode to 'WholePage'
```

## [1.2.80] - 15-June-2020
### Added
- Added ability to collapse the property panel pages using chevron icon.
- Annotation/Form editor: added snap alignment. (DOC-1882)  
  By default, snap alignment is enabled, the default snap tolerance is 5pt both vertically and horizontally. 
  Default snap margin is 10pt. Snap to center is also enabled.
  Press Alt key if you wish to temporarily disable snap during resize or move action.  
  Use snapAlignment option if you wish to customize snap alignment. Some examples:  
```javascript
  // Change snap alignment tolerance:
  var viewer = new GcPdfViewer("#root", { snapAlignment: { tolerance: 25 },  supportApi: 'api/pdf-viewer'});
  // Set vertical and horizontal alignment tolerance individually:
  var viewer = new GcPdfViewer("#root", { snapAlignment: { tolerance: { vertical: 10, horizontal: 50 } },  supportApi: 'api/pdf-viewer'});
  // Disable snap to center of the element:
  var viewer = new GcPdfViewer("#root", { snapAlignment: { center: false},  supportApi: 'api/pdf-viewer'});
  // Enable snap to center of the element for vertical alignment only.
  var viewer = new GcPdfViewer("#root", { snapAlignment: { center: {vertical: true, horizontal: false, } },  supportApi: 'api/pdf-viewer'});
  // Disable snap margin:
  var viewer = new GcPdfViewer("#root", { snapAlignment: { margin: false },  supportApi: 'api/pdf-viewer'});
  // Enable vertical snap margin:
  var viewer = new GcPdfViewer("#root", { snapAlignment: { margin: { vertical: 10, horizontal: false} },  supportApi: 'api/pdf-viewer'});
  // Disable snap alignment feature:
  var viewer = new GcPdfViewer("#root", { snapAlignment: false,  supportApi: 'api/pdf-viewer'});
```

- Annotation/Form editor: arrow keys move the current element, Shift-arrow resize the element. (DOC-1955)

- Viewer search improvements. (DOC-1885)
  * Proximity search
  * Starts with/ends with
  * Wildcards

- Annotation/Form editor: Added the ability to copy and paste annotations or fields. (DOC-1881)  
  Related methods and properties:
```javascript
  // Execute Copy action  (Ctrl+C shortcut).
  // @param buffer Optional. Data to copy.
  execCopyAction(buffer?: CopyBufferData): Promise<boolean>

  // Execute Cut action (Ctrl+X shortcut).
  // @param buffer Optional. Data to cut.
  execCutAction(buffer?: CopyBufferData): Promise<boolean>

  // Execute Paste action (Ctrl+V shortcut).
  // @param point Optional. Insert point, note, pageIndex should be specified.
  execPasteAction(point?: GcSelectionPoint): Promise<boolean>

  // Execute Delete action (DEL shortcut).
  // @param buffer Optional. Data to delete.
  execDeleteAction(buffer?: CopyBufferData): Promise<boolean>

  // Returns selected text.
  getSelectedText(): string

  // Indicates whether the buffer contains any data.
  get hasCopyData: boolean
```

- Annotation/Form editor: Added the ability to clone the current annotation/field.
```javascript
  // Clone annotation or field given by parameter annotation.
  cloneAnnotation(annotation: AnnotationBase)
```

- Added text annotation reply tool (opens on the right side of the viewer). To enable, use the addReplyTool() method. (DOC-1884)  
```javascript
  // Enable the Text Annotation Reply Tool.
  // Note that to enable adding, removing or editing replies, the SupportApi must be configured.
  // Otherwise, the reply tool opens in read-only mode.
  // @param sidebarState Optional. Pass 'expanded' value if you wish the Reply tool to be expanded initially. Default value is collapsed.
  addReplyTool(sidebarState: GcRightSidebarState = 'collapsed')

  // Indicates whether the Reply Tool has been added.
  get hasReplyTool(): boolean

  // Gets right sidebar object. Use this object if you want to manipulate the right sidebar.
  get rightSidebar(): GcRightSidebar

  // Usage example:
  var viewer = new GcPdfViewer("#root");
  viewer.addReplyTool('collapsed');
  viewer.rightSidebar.show('expanded', 'reply-tool');
  viewer.rightSidebar.hide();
  viewer.rightSidebar.expand();
  viewer.rightSidebar.collapse();
  viewer.rightSidebar.toggle();
```


- Added viewer context menu and the ability to customize it. Related properties and methods:
```javascript
  // By default, the viewer uses its own context menu.
  // Set this option to true if you want to use the browser context menu.
  // Please, note that if this option is set to true, some functions of the
  // context menu will not be available (for example, actions of the Editor and Reply tool).
  // The default value is false.
  useNativeContextMenu: boolean;

  // This handler function will be called when the context menu is about to be shown.
  // Function arguments:
  //   items: DropdownItem[],                 /* Modify this array if you wish to change the current set of menu items */
  //   mousePosition: {x: number, y: number}, /* The mouse position */
  //   viewer: GcPdfViewer                    /* The owner GcPdfViewer */
  // You can use this function to customize the context menu.
  // Return false if you want to prevent the context menu from opening.
  onBeforeOpenContextMenu: Function;

  // This handler function will be called when the context menu is about to be hidden.
  // Function arguments:
  //   viewer: GcPdfViewer                    /* The owner GcPdfViewer */
  // Return false if you want to prevent the context menu from closing.
  onBeforeCloseContextMenu: Function;

  // This code shows how to modify the context menu and add search using Google and Bing search engines:
  viewer.options.onBeforeOpenContextMenu = function (items, mousePosition, viewer) {
      var selectedText = viewer.getSelectedText();
      if (selectedText) {
          // Remove existent items:
          items.splice(0, items.length);
          // Add own menu items:
          items.push({
              type: 'button',
              text: 'Search using Google',
              onClick: function () {
                  window.open('http://www.google.com/search?q=' + encodeURI(selectedText), '_blank');
              }
          });
          items.push({
              type: 'button',
              text: 'Search using Bing',
              onClick: function () {
                  window.open('https://www.bing.com/search?q=' + encodeURI(selectedText), '_blank');
              }
          });
      }
      return true;
  };
```

- Added editorDefaults option, use this option if you wish to change some default editor values.  
  For example you can change the default sticky note color:
```javascript
  var viewer = new GcPdfViewer("#root", { editorDefaults: { stickyNote: { color: '#FF0000'} },  supportApi: 'api/pdf-viewer'});
  // This is the default value for the editorDefaults option:
  {
      textAnnotation: {
          color: '#ffdc38',
          contents: ''
      },
      stickyNote: {
          color: '#38e5ff',
          contents: ''
      }
  }
```

- Show the total number of results in the search panel. (DOC-2068)

### Changed
- Text annotation: group all replies to an annotation together as threaded comments. (DOC-2023)
- Form editor: field captions in the list now show field names.
- Added the ability to collapse the property panel pages using the chevron icon.


## [1.1.70] - 11-June-2020
### Fixed
- Form editor close icon is not shown when the editor is opened using the toolbar button. (DOC-2048)
- Default border width value for new checkbox and radio button fields changed from 1 to 0. (DOC-1978)

## [1.1.69] - 31-May-2020
### Fixed
- Incorrect sticky note icon appearance. (DOC-2010)
- Incorrect file extension when downloading a PDF using the "save" button in IE11
  if the HTTP response contains content-disposition header in modern format. (DOC-2009)
- (Safari) Checkbox field cannot be checked. (DOC-2013)
- friendlyFileName option does not affect the file name in the properties dialog. (DOC-2019)
- Cannot rename radio button field to same name to create radio button group. (DOC-2025)
- Pressing the Tab key on the last form field on a page goes back to first form field in the page
  instead of the first control on the next page. (DOC-2024)
### Changed
- Auto-generated IDs in the Annotation Editor simplified.

### Added
- Annotations/From editor: added UI notification when opening a PDF that does not allow editing. (DOC-1977)
- Added new properties: fileUrl, fileName. (DOC-2019)
```javascript
    // Gets the URI that was used to open the document.
    // Returns an empty string when the document was opened from binary data.
    get fileUrl(): string

    // Gets the file name that was used to open the document.
    // The file name is determined as follows:
    // - if a local file was opened using the "Open File" dialog, returns the local file name;
    // - else, if a new file was created using the "newDocument" method, returns the argument passed to that method;
    // - else, if options.friendlyFileName is not empty, returns its value;
    // - else, returns the name generated from the URL passed to the "open" method.
    get fileName(): string

    // Usage example:
    viewer.onAfterOpen.register(function() {
        alert("Opened document, fileUrl: " + viewer.fileUrl + ", fileName: " + viewer.fileName);
    });
    viewer.open('/Annotations.pdf');
```
## [1.1.66] - 08-May-2020
### Fixed
- No response when the "download" button is clicked in IE11. (DOC-2007)
- FreeText annotation's text box size is incorrect when annotation
  is updated using the annotation editor in some cases. (DOC-1990)

## [1.1.65] - 07-May-2020
### Fixed
- Filename is missing the .pdf extension when the "download" tool is used to download the PDF. (DOC-2006)

## [1.1.64] - 05-May-2020
### Fixed
- Cannot rename a field if its name ends with an underscore and a digit. (DOC-2001)
- Incorrect default field names. (DOC-2002)
- Method 'scrollAnnotationIntoView' scrolls the view even if the annotation is already. (DOC-2003)
### Changed
- Default value for new text fields' border color changed to black. (DOC-1978)

## [1.1.63] - 04-May-2020
### Fixed
- Tab doesn't work if you want to focus the field from last to first field. (DOC-1987)
- The size property of FileAttachment annotation is incorrect after undo. (DOC-1988)
- Some PDFs are displayed incorrectly. (DOC-1990)
- File attachments still show after deleting. (DOC-1973)
- "Unknown Widget" is shown after adding a text field with a duplicate name. (DOC-1991)
- Deleting a form field could delete just the widget, leaving the field. (DOC-1991)
- The Combs of Comb text field are not displayed. (DOC-1989)
- Form field values are not saved when the field's owner is another field. (DOC-1997)
- Forecolor applies to border as well. (DOC-1963)
- Signature is not visible in the thumbnail. (DOC-2000)
### Changed
- SupportApi: show an error message when an unlicensed copy of GcPdf is used. (DOC-1818)
- 'Foreground color' property name changed to 'Forecolor' for Text Fields. (DOC-1964)

## [1.1.62]
### Fixed
- Problem with updating form fields tab order. (DOC-1982)
- Fields reordering issues. (DOC-1984)
- Default value for Text Field border width changed to 1. (DOC-1978)
- The default zoom mode becomes "Fit to page" when option restoreViewStateOnLoad is set to false. (DOC-1981)
### Added
- userData option: Arbitrary data associated with the viewer. This data is sent to the server when the document is saved.

## [1.1.61]
### Fixed
- Incorrect rendering of redact annotations in some PDFs. (DOC-1976)
- Text field border width controls in panel not working correctly. (DOC-1979)

### Changed
- Pdf viewer migrated from Preact to React. (DOC-1886)

## [1.1.60]
### Fixed
- Incorrect signature rendering. (DOC-1974)
- The thumbnail is not focused. (DOC-1972)
- addDocumentListPanel method: fixed problem when documents list opened inside WebForms application.
- event onBeforeOpenFile is not raised when document is opened using ReportDocumentMoniker instance.
### Added
- new method: loadDocumentList() // Load updated document list into document list panel.
- new option: author // Optional. Default author name. The option is used by Annotation Editor as default value for 'author' field.

## [1.1.59]
### Fixed
- Focused property element is not retained after property change. (DOC-1912)
- (Support API) Error message is shown when opening a password-protected file on server. (DOC-1946)

## [1.1.57]
### Fixed
- Text field background color value is not loaded into property list. (DOC-1872)
- FontFamily is not applied to text fields. (DOC-1861)
- Text justification is not applied to a text field widget when a modified document is saved and loaded into the viewer again. (DOC-1873)
- Focused property element is not retained after property change. (DOC-1912)
- Some GcPdfViewer buttons fire form submit when running inside a WebForms application. (DOC-1922)
- Fixed problem with resolving download action for Web Api 2 (WebForms) routing.
- Pan tool is not activated in some cases.
### Added
- Text fields: added support for font color and font size.
- Added new method saveChanges: uploads local changes to server without file download:
```javascript
    saveChanges(): Promise<boolean>
```
- Added new method submitForm: submits form to the server.
```javascript
    submitForm(submitUrl: string): void
```
- Added new property: canEditDocument // boolean, indicates whether opened document can be edited using SupportApi.
- Localization for progress dialog, property list, thumbnails.
### Changed
- Returned value for method save() changed from void to Promise<boolean>

## [1.1.56]
### Fixed
- Incorrect thumbnail scale when browser zoom level changed. (DOC-1869)
### Changed
- Properties dialog: close button style updated, added missed translation keys

## [1.1.55]
### Fixed
- Fixed: properties panel does not collapse when button 'back to view tools' clicked in some cases.
- ColorUtils, hexToRgb: fixed problem with incorrect color conversion when color argument is specified using rgb model.
### Added
- Properties dialog localization.

### Changed
- Properties dialog style updated.
- Annotation/form editor sidebar behavior updated.

## [1.1.54]
### Fixed
- Link annotation can be navigated in editor mode. (DOC-1848)
- Sidebar pinned state is not retained when switching from editor mode to viewer mode. (DOC-1849)
### Added
- Localization support. (DOC-993)
- Display error message when document size exceeds SupportApi server size limit.
- Added ability to edit basic Link annotation properties. (DOC-1847)
- Added new property: viewer.zoomValue // number, gets/sets the current zoom percentage level.

## [1.1.53]
### Fixed
- Searching for text with whole word on does not work in some cases. (DOC-1824)
### Added
- Added ability to open annotation or form editor when no document is loaded. (DOC-1804)
- supportApi option: added ability to suppress error and information messages about Support API availability. (DOC-1806)
  Example:
```javascript
var viewer = new GcPdfViewer('#root', {supportApi: {apiUrl: 'api/pdf-viewer', suppressInfoMessages: true, suppressErrorMessages: true }});
```
- Added new property:
```javascript
get hasDocument(): boolean // Returns true if a document is loaded
```
- Added the ability to change annotation printable flag. (DOC-1841, DOC-1840)
- Text fields: added 'Required' property.
- Delete existing, or add a new empty page. (DOC-1699)
- Added confirmation for large attachments (> 4MB). (DOC-1745)
- Added ability to hide signature widgets when 'hide-annotations' button is checked.
- Added new option coordinatesOrigin: the coordinate system into which objects are transformed before they are displayed.
  Possible values are 'TopLeft' and 'BottomLeft'. Default value is 'TopLeft'. This option is used by Annotation/Form editors.
  Example:
```javascript
var viewer = new GcPdfViewer("#root", { coordinatesOrigin: 'BottomLeft', supportApi:"api/gcpdfviewer" });
```
- Added new option watermarkBanner: specifies watermark text which will be shown at the bottom/right corner of the viewer.
Example:
```javascript
var viewer = new GcPdfViewer('#root', { watermarkBanner: "GrapeCity PDF Viewer v{{version}}." });
```
- Added new event onAfterOpen: occurs when a document has been opened.
Example:
```javascript
var viewer = new GcPdfViewer('#root');
viewer.onAfterOpen.register(function() {
  console.log("Document opened.");
});
viewer.open('Test.pdf');
```
- Added Signature annotation verification (available only when supportApi option is set). (DOC-999)
- Added Form editor. (DOC-1597)
- Added Annotation Editor. (DOC-1581, DOC-1582, DOC-1093, DOC-1589, DOC-1588, DOC-1579, DOC-1591, DOC-1599, DOC-1590, DOC-1592, DOC-1580, DOC-1585)
- Added client API to edit annotations programmatically. (DOC-1587)
  - New properties:
```javascript
    // Gets all document annotations.
    get annotations(): Promise<{ pageIndex: number, annotations: AnnotationBase[] }[]>

    // Returns true if document has been changed by user.
    get hasChanges(): boolean

    // Gets a value indicating whether the pdf viewer can undo document changes.
    get hasUndoChanges: boolean

    // Gets a value indicating whether the pdf viewer can redo document changes.
    get hasRedoChanges: boolean

    // Gets current undo changes index.
    get undoIndex(): number

    // Gets total undo changes count.
    get undoCount(): number

    // Gets or sets more precise Edit mode for Annotations or Form editor.
    get/set editMode(): EditMode

    // Gets or sets the layout mode (Viewer, AnnotationEditor or FormEditor).
    // Default layout mode is Viewer.
    get/set layoutMode(): LayoutMode

    // Defines toolbar layout for different viewer modes: viewer, annotationEditor,formEditor.
    // Usage example:
    // viewer.toolbarLayout.viewer.default = ["open", "save"];
    // viewer.toolbarLayout.annotationEditor.default = ["open", "save", "$split", "new-document", "edit-ink", "edit-text"];
    // viewer.applyToolbarLayout();
    get toolbarLayout(): GcPdfViewerToolbarLayout
```
  - New methods:
```javascript
    // Call this method in order to apply changed toolbarLayout.
    applyToolbarLayout()

    // Add annotation to document.
    addAnnotation(pageIndex: number, annotation: AnnotationBase): Promise<boolean>

    // Update annotation.
    updateAnnotation(pageIndex: number, annotation: AnnotationBase): Promise<boolean>

    // Remove annotation from document.
    removeAnnotation(pageIndex: number, annotationId: string): Promise<boolean>

    // Select annotation for editing.
    // @param pageIndex Zero-based page index.
    // @param annotation Id or Annotation object itself.
    selectAnnotation(pageIndex: number, annotation: AnnotationBase | string): Promise<boolean>

    // Reset annotation selection.
    unselectAnnotation()

    // Scroll annotation into view.
    scrollAnnotationIntoView(pageIndex: number, annotation: AnnotationBase)

    // Undo last document change.
    undoChanges()

    // Redo next document change.
    redoChanges()
```
```javascript

    // Changes coordinate system origin for a rectangle given by parameter
    // bounds and returns rectangle value converted to a new coordinate system origin;
    // @param pageIndex Page index (Zero based).
    // @param bounds Bounds array: [x1, y1, x2, y2].
    // @param srcOrigin Source coordinate system origin. Possible values are: 'TopLeft' or 'BottomLeft'.
    // @param destOrigin Destination coordinate system origin. Possible values are: 'TopLeft' or 'BottomLeft'.
    changeBoundsOrigin(pageIndex: number, bounds: number[],
                      srcOrigin: 'TopLeft' | 'BottomLeft', destOrigin: 'TopLeft' | 'BottomLeft'): number[]

    // Returns PDF page's view port information.
    // @param pageIndex Zero-based page index.
    // @returns object containing following fields:
    //   {
    //     viewBox: number[],                // Original page bounds: [x1, y1, x2, y2].
    //                                       // If you want to know original page's width/height, you can get it using viewBox values:
    //                                       // var pageWidth  = viewBox[2] - viewBox[0];
    //                                       // var pageHeight = viewBox[3] - viewBox[1];
    //     width: number,                    // Current width of the page in user space (scale and rotation values are applied),
    //     height: number,                   // Current height of the page in user space (scale and rotation values are applied)
    //     scale: number,                    // Active scale value
    //     rotation: number,                 // Active rotation value
    //  }
    getViewPort(pageIndex: number)

    // Update set of annotations which is located on one page.
    // @param pageIndex
    // @param annotations
    // @returns Promise, resolved by updated annotation objects.
    public updateAnnotations(pageIndex: number, annotations: AnnotationBase | AnnotationBase[]): Promise<{ pageIndex: number, annotations: AnnotationBase[] }>

    // Update radio buttons group given by parameter fieldName with new field value.
    // @param fieldName Grouped radio buttons field name
    // @param newValue New fieldValue
    // @param skipPageRefresh boolean. Set to true if you don't need to update page display. Default is false.
    // @returns Promise, resolved by boolean value, true - radio buttons updated, false - some error occurred.
    updateRadioGroupValue(fieldName: string, newValue: string, skipPageRefresh?: boolean): Promise<boolean>

    // Find annotation(s) within opened document.
    // Returns promise which will be resolved with search results.
    findAnnotation(findString: string, findParams?: { findField?: 'id' | 'title' | 'contents' | 'fieldName' | string,
                                                      pageNumberConstraint?: number,
                                                      findAll?: boolean
    }): Promise<{ pageNumber: number, annotation: AnnotationBase }[]>

     // Example: find annotation with id '2R':
        viewer.findAnnotation("2R").then(function(annotation) {  if(annotation)    alert('Annotation found.');  else    alert('Annotation not found.');  });

     // Example: find annotation with title 'Some Title':
        viewer.findAnnotation("Some Title", {findField: 'title'}).then(function(annotation)  {    } );

     // Example: find all field widgets with name field1:
      viewer.findAnnotation("field1", {findField: 'fieldName', findAll: true}).then(function(dataArray) {
        if(dataArray.length > 0) {
          alert('Found ${dataArray.length} fields, value of the first field is ${dataArray[0].annotation.fieldValue}');
        } else {
          alert('field1 not found.');
        }
      });
```
- Added additional user-friendly page navigation methods. (DOC-1612)
```javascript
  // Navigate page with specific page number.
  goToPageNumber(pageNumber: number)

  // Navigate first page.
  goToFirstPage()

  // Navigate previous page.
  goToPrevPage()

  // Navigate next page.
  goToNextPage()

  // Navigate last page.
  goToLastPage()
```
```javascript
  // Scroll page into view.
  scrollPageIntoView(params: { pageNumber: number; destArray?: any[]; allowNegativeOffset?: boolean; })
  // @param params object, with parameters:
  //   pageNumber - number. Page number.
  //   destArray - optional. Array with destination information:
  //     destArray[0] - not used, can be null, pdf page reference (for internal use only).
  //     destArray[1] - contains destination view fit type name:
  //       { name: 'XYZ' }   - Destination specified as upper-left corner point and a zoom factor.
  //       { name: 'Fit' }   - Fits the page into the window
  //       { name: 'FitH' }  - Fits the width of the page into the window
  //       { name: 'FitV' }  - Fits the height of the page into a window.
  //       { name: 'FitR' }  - Fits the rectangle specified by its upper-left and lower-right corner points into the window.
  //       { name: 'FitB' }  - Fits the bounding box into the window (fits the rectangle containing all visible elements on the page into the window).
  //       { name: 'FitBH' } - Fits the width of the bounding box into the window.
  //       { name: 'FitBV' } - Fits the height of the bounding box into the window.
  //     destArray[2] - x position offset
  //     destArray[3] - y position offset (note, the lower-left corner of the page is the origin of the coordinate system (0, 0))
  //     destArray[4] - optional, can be null, contains bounding box width when view name is FitR, contains scale when view name is XYZ,
  //     destArray[5] - optional, can be null, contains bounding box height when view name is FitR
  // @param allowNegativeOffset optional, boolean, true when negative page offset should be  allowed.
  //
  // Examples:
  // Scroll page into view:
  viewer.scrollPageIntoView( { pageNumber: 10 } )
  // Scroll annotation into view, example:
  var rectangle = annotation.rect;
  var pagePosX = rectangle[0];
  var pagePosY = rectangle[1] + Math.abs(rectangle[3] - rectangle[1]);
  var pageScale = viewer.zoom.mode === ZoomMode.Value ? viewer.zoom.factor / 100.0 || 1.0;
  viewer.scrollPageIntoView({ pageNumber: pageIndex + 1, destArray: [null, { name: "XYZ" }, pagePosX, pagePosY, pageScale] });
```
- Added editor shortcuts:
      Del: Delete selected annotation.
      Esc: Unselect annotation.
      Ctrl Z: Undo changes.
      Ctrl Shift Z: Redo changes.
      Ctrl Y: Redo changes.

## [1.0.44]

### Fixed
- Incomplete form gets submitted when the PDF form has multiple pages. (DOC-1688)

### Added
- Document security: added support for user access restrictions which is defined in PDF document
  (print/text copying/acro-form filling).
- Document Properties dialog: added 'Security' tab. (DOC-1372)

## [1.0.42]

### Fixed
- Initial view mode does not set to 'Continuous View' in some cases. (DOC-1488)
- Current page thumbnail does not scroll into view. (DOC-1435)

### Changed
- Outline panel: button title changed to "Bookmarks".
- Outline panel: added ability to collapse outline items.
- Attachments panel: added ability to open PDF attachments in the viewer instead of downloading the file.

### Added:
- New event onBeforeOpen: occurs immediately before document open.
```javascript
// Usage example:
var viewer = new GcPdfViewer('#root');
viewer.onBeforeOpen.register(function(args) {
  alert("A new document will be opened,\n payload type(binary or url): " + args.type +",\n payload(bytes or string): " + args.payload);
});
viewer.open('Test.pdf');
```

## [0.7.29]

### Fixed
- Text outside of the document cannot be selected. (DOC-1413)
- Next page button does not scroll to the page if part of it is already visible. (DOC-1414)
- The icon button in navigation panel don't fully display in edge browser. (DOC-1398)
- (iOS) Document content disappears when changing zoom. (DOC-1408)
- When a form is posted, incorrect values are sent for some radio buttons. (DOC-1388)

## [0.7.28]

### Fixed
- When a form is posted, incorrect names are sent for combo and list boxes. (DOC-1388)

## [0.7.27]

### Added
- Document properties and fonts dialog (toolbar button name 'doc-properties'). (DOC-1339)

## [0.7.24]

### Fixed
- Thumbnails show incorrectly for non-standard page sizes. (DOC-1346)
- Incorrect page orientation in print preview in some cases. (DOC-1347)

## [0.7.23]

### Fixed
- The zoom out button disappears in Internet explorer. (DOC-1333)
- License icon does not show fully in Edge browser. (DOC-1302)
- Radio button and checkbox cannot be selected in Edge browser. (DOC-1336)
- FreeText callout lines are not shown when the annotation's appearance is specified by an appearance stream. (DOC-1317)

### Changed
- If a FreeText annotation has an appearance stream, but it includes text that requires an external CMap
    that cannot be located, the text specified in the annotation is shown instead of the appearance stream. (DOC-1317)

### Added
- Added the ability to hide FreeText annotations using the 'Hide Annotations' button.
- Added support for CID-keyed fonts with external CMap tables. (DOC-1328)
  New options:
  - cMapUrl - in some cases, predefined character map files are needed to show CJK text output.
    The cMapUrl option specifies the URL of the folder where the CMap files are located. The default value is 'resources/bcmaps/'.
    A CMap specifies the mapping from character codes to character selectors, it is used to extract Unicode text from a PDF document.
    In most cases the CMaps are fully embedded in the PDF document, but sometimes a CMap in a PDF is specified by a PDF name object, where this name identifies a predefined CMap that should be known to the PDF processor.
    The compressed predefined CMaps that are included with GcPdfViewer are located in the 'resources/bcmaps/' folder, and have the .bin extension.
  - cMapPacked - if true, the viewer will look for the compressed version of the CMap files (with the extension '.bin'), otherwise the viewer will look for unpacked CMap files without extension. The default value is true.

## [0.7.21]

### Fixed
- Fixed the Zoom button in IE. (COREUI-14)
- Fixed the Clear button in SearchPanel. (ARJ-650)
- PDF content is not displayed correctly. (DOC-1317)

### Changed
- Zoom In/Out buttons behavior correction: The zoom step is now 10% for zoom factors below 100%,
  and 25% for zoom factors above 100%. Also added 150% zoom value. (ARJ-606)

## [0.7.20]

### Added
- Added npmjs package. (DOC-1301)

### Fixed
- Navigation toolbar buttons are not working properly when document is opened using Ctrl+O.

## [0.7.19]

### Fixed
- License error message: fixed problem with icon, added line breaks. (DOC-1296)
- License error message shown twice in some cases. (DOC-1297)
- Cannot edit List box form control in GcPdfViewer. (DOC-1235)
- 'Go To Last' button hidden by default in "Toggle FullScreen" view. (DOC-1232)
- Search with "whole word" option result is not correct when it is a form data. (DOC-1233)
- Highlight area is not correct in some scene. (DOC-1257)
- Prompt "invalid license key" when I set evaluation 365 days key for GcPdfViewer. (DOC-1297)
- The appearance of the watermark is not correct. (DOC-1295)
- Script gcpdfviewer.vendor.js is no longer required

## [0.7.18]

### Added
- Licensing behavior updated. (DOC-1282)

## [0.7.17]

### Fixed
- 'Hide-annotations' does not hide Normal appearance stream of the redact annotation. (DOC-1283)

## [0.7.16]

### Added
- Support Normal/Rollover/Down appearance streams for REDACT annotations. (DOC-1283)

## [0.7.15]

### Added
- GcPdfViewer does not work when scripts loaded from the head tag. (DOC-1286)

## [0.7.14]

### Added
- Added licensing. (DOC-1282)
  Example of how to create GcPdfViewer with product license:
  ```javascript
  <script>
    // Add your license
    GcPdfViewer.LicenseKey = 'xxx';
    // Add your code
    window.onload = function(){
      const viewer = new GcPdfViewer("#viewer");
      viewer.addDefaultPanels();
    }
  </script>
  ```
- Added ability to track previously opened PDF file.
  Note that only files opened from URI can be tracked (not from binary data).
  Use restoreViewStateOnLoad option in order to restore a previously opened file on next viewer load:
  ```javascript
    { restoreViewStateOnLoad: true }
    // or
    { restoreViewStateOnLoad: { trackFile: true } }
  ```

## [0.7.13]
### Fixed
- Sidebar animation during restore view state disabled
- Document title toolbar item - hover style removed

## [0.7.12]

### Fixed
- Restore previously opened sidebar correction: do not expand disabled sidebar panel on viewer load.
- Search character * in PDF viewer the search result is not correct. (DOC-1026)
- Correction: Search highlight is not fully visible when it is located at a page bottom edge
- Fixed regression problem with double click selection (added in build 0.7.6)

### Changed
  Double-click time increased from 300ms to 500ms.

## [0.7.11]

### Fixed
- Clear search highlight when search panel closed
- Focus search query input when search panel opened
- Restore previously opened sidebar panel on document load (see restoreViewStateOnLoad option).
- There are no response when click "More Results" button after search text. (DOC-1178)

## [0.7.10]

### Fixed
- Popup title appearance correction: underline now hidden when popup content is empty.
- Fixed: The modification date (if it is available) is not displayed in the popup correctly.
- Z order of redact annotation changed after loaded by JS PDF Viewer. (DOC-1177)

## [0.7.9]

### Added
- New toolbar button: 'About'.
- Redact annotations. (DOC-1206)
- New option: hideAnnotationTypes - specifies annotation types which will be hidden when the 'hide-annotations' button is checked.
```javascript
// Default value:
['Text', 'FreeText', 'Line', 'Square', 'Circle', 'Polygon', 'Polyline', 'Ink', 'Popup',
'Sound', 'Polygon', 'RadioButton', 'Checkbox', 'PushButton', 'Choice', 'TextWidget', 'Redact']
// Possible values are:
['Text',  'Link',  'FreeText',  'Line',  'Square',  'Circle',  'Polygon',  'Polyline',  'Ink',  'Popup',  'FileAttachment',
'Sound',  'ThreadBead',  'RadioButton',  'Checkbox',  'PushButton',  'Choice', 'TextWidget',  'Redact']
// or 'All' or 'None'
// Examples:
let options = { hideAnnotationTypes: 'All' }; // Hide all possible annotations.
let options = { hideAnnotationTypes: ['PushButton', 'Redact'] }; // Hide Push button and Redact annotations only.
```
- New property: version - returns current viewer version.
- Added loading icon indicator for doc-title toolbar item.

## [0.7.8]

### Added
- Added new toolbar button: Hide annotations, button key is 'hide-annotations',
  use this button if you wish to preview original document without annotations,
  this can be useful when you view a redacted document.
  Annotations that are affected by the 'hide-annotations' button:
  Redact, Sound, Text, FreeText, Line, Square, Circle, Polyline, Ink, Polygon, RadioButton, Checkbox, PushButton, Choice.
  Annotations that are not affected by the 'hide-annotations' button:
  FileAttachment, Article thread bead, Link, Signature, Caret, Highlight, Underline, Stamp, Squiggly, StrikeOut, Popup.

## [0.7.7]

### Added
- Support redact annotations. (DOC-1206)

## [0.7.6]

### Added
- Display shortcuts in toolbar button tooltips.

### Fixed
- Text still selected after mouse click empty area. (DOC-1182)
- Highlight area is not correct when search text contains "-". (DOC-1199)

## [0.7.5]

### Fixed
- Fixed regression issue with selected text copy feature using Ctrl+C (added in build 0.7.3)

## [0.7.4]

### Fixed
- Thumbnails panel performance with large pdf. (DOC-1231)

## [0.7.3]

### Fixed
- restoreViewStateOnLoad option fix:
  Now the zoom is restored immediately without waiting for the document load.
  Note that trackScale option should be set to true: `restoreViewStateOnLoad: {trackScale: true}`
- R/Shift+R shortcuts do not work after clicking on toolbar/sidebar.

## [0.7.2]

### Added
- New method applyOptions: Use this method in order to propagate changed options. Example:
```javascript
var viewer = new GcPdfViewer(selector);
viewer.options.buttons = ['print', 'rotate'];
viewer.applyOptions();
```

### Fixed

- Unable to set viewer buttons using the 'buttons' option.
- The Open method does not load PDF file in IE11.
- Appearance for interactive checkbox and radio buttons updated.

## [0.7.1]

### Changed
- Now pdf viewer throws exception when trying to load a large file (>1000 pages) without specifying background worker (see workerSrc option). (DOC-1230)

## [0.7.0]

### Fixed
- Viewer hangs when a large linearized PDF is loaded. (DOC-1230)

## [0.6.9]

### Fixed
- Fixed: thumbnails not shown in IE11.
- Fix regression bug with thumbnails panel activation (added in build 0.6.7).

## [0.6.8]

### Fixed
- Button boundary and background color are not visible. (DOC-1207)
- Performance issues when using mouse wheel when a large (>1000 pages) PDF is loaded. (DOC-1194)

## [0.6.7]

### Changed
- Font icons replaced by svg icons for toolbar and sidebar.
- Show file name in the viewer. (DOC-1191)

## [0.6.6]

### Fixed
- Form data can not be searched. (DOC-1173)
- Default toolbar buttons order changed:
```javascript
['open', '$navigation', '$split', 'textselection', 'pan', '$zoom', '$fullscreen', 'rotate', 'view-mode', 'theme-change', 'print', 'save', 'doc-title']
```

## [0.6.5]

### Fixed
- Form fields not editable on loading PDF acroform in IE/IE Edge. (DOC-1184)
- ReportListPanel renamed to DocumentListPanel
- Cannot see the choice of list box in print preview clearly. (DOC-1170)
- Extra outline when text widget annotation is focused removed. (DOC-1171)
- Preparing document for printing dialog invoked by "Ctrl + P" hotkey is different from which invoked by push "Print Document" button in the toolbar. (DOC-1181)
- Cannot quit "Loading" state after cancel input password of the pdf file. (DOC-1198)
- "Ctrl + P" hot-key can not take effect if push "Print Document" and cancel print before. (DOC-1185)
- Text still selected after mouse click empty area in the page. (DOC-1182)
- Password value show after load pdf which has text field that set password property to true. (DOC-1186)
- Click outline node has no response which action is navigate to a web site. (DOC-1187)
- Always show "Continuous View" tool tip no matter it is in single page view mode or continuous view mode. (DOC-1179)
- Some form data lost after loaded by JS PDF viewer. (DOC-1169)
- The search dialog is replaced by Search Panel in sidebar. (DOC-1180)
- Can not select text in forms on IE browser. (DOC-1202)
- Text style is not correct in search result. (DOC-1201)
- Can not see attachments after load attach pdf file which has attachments files. (DOC-1188)
- Can not go to destination of the outline node correctly. (DOC-1193)
- Highlight area is not correct when search text contains "-". (DOC-1199)
- Highlight area is not correct in certain scene. (DOC-1200)
- Page Size is not correct when print. (DOC-1192)

### Changed
- restoreViewStateOnLoad option: added ability to track theme.

### Added
- New option documentListUrl: Url to document list service used by DocumentListPanel
  The service should return json string with available documents array, e.g.: ["pdf1.pdf", "pdf2.pdf"].
```csharp
  // ASP .NET service back-end action sample :
  [HttpGet("documents")]
  public ActionResult Documents()
  {
    var doucmentsFolder = new System.IO.DirectoryInfo("pdfdocs");
    var files = from file in doucmentsFolder.EnumerateFiles("*.pdf") select file.Name;
    return new ObjectResult(files.ToArray());
  }
```

## [0.6.4]

### Added
- Page title will now be shown under thumbnail.
- New toolbar buttons: text selection/pan
- Current article thread indication
- pdf.js updated to latest version:
  - Added CaretAnnotation support
  - SVG renderer enhanced (see renderer option)
  - Show modification date for annotation pop-ups if it is available and can be parsed correctly (date string should be in ISO 8601 standard)
- Added option theme: Use this option to change the default viewer theme.
  ```javascript
  // Example:
  var viewer = new GcPdfViewer(selector, { renderInteractiveForms: true, theme: 'themes/light-blue' });
  ```
- Added option restoreViewStateOnLoad: Track viewer state changes, save state to local storage and restore on first load. Set this option to false if you wish to disable viewer state changes tracking.
- Added method setTheme(): use to change the active theme.
- Added property rotation: number, use to get/set the current document rotation in degrees.
- Added even onError: indicates error event.
  ```javascript
  // Event arguments:
    { message: string, source: GcPdfViewer, type: 'open' | string, exception?: any }
  // Usage example:
    var viewer = new GcPdfViewer("#root", { keepFileData: true });
    viewer.onError.register(handleError);
    function handleError(eventArgs) {
      if (eventArgs.message.indexOf("Invalid PDF structure") !== -1) {
        var message = eventArgs.message;
        var fileData = eventArgs.source.fileData;
        if (fileData) {
          message = new TextDecoder('utf-8').decode(fileData);
        }
        eventArgs.source.reportError({ message: message, severity: "warn" });
      }
    }
  ```

### Fixed
- Thread beads should be hidden during printing. (DOC-1163)
- Page number is not correct in the toolbar when read as article thread mode. (DOC-1164)
- (IE11/Edge) Fixed problem with article threads. (DOC-1167)
- Do not draw border when color is not specified even if style and width specified. (DOC-1172)
- Can not input data in form control under IE and edge browser. (DOC-1176)
- Can not input data in form control when select pan tool in the toolbar. (DOC-1166)
- Do not jump to other page when rotate page. (DOC-1165)
- Fixed: S, H keyboard shortcuts does not update toolbar.
- Fixed: Open SearchPanel using Ctrl+F.
- Fixed problem with thumbnails title when document contains page labels
- Fixed problem with externalLinkTarget option.
- Document title loading text correction - show file name instead of full URL.
- Article threads: by default article title should be empty
- Fixed problem with article threads navigation when one page contains more than one overlapped beads from different articles.
- Added error handling for Reports List fetch.

## [0.6.3]

### Added
- New method addDefaultPanels: Add default set of sidebar panels with default order.
```javascript
// Usage example:
const viewer = new GcPdfViewer("#viewer1", { file: 'file1.pdf' });
viewer.addDefaultPanels();
```

### Changed
- Pages rendering priority updated:
  navigated page takes priority over other pages, even when this page is less visible compared to others.
- Zoom using two finger gesture for mobile devices.

### Fixed
- Fixed several IE 11 issues.
- Fixed problem with context menu for selected text.
- Behavior for sidebar buttons state updated.
- Behavior of the canvas selection for paragraph corner corrected.
- Fixed exception when another document loaded

## [0.6.2]

### Fixed
- Caret behavior for Ctrl+Home, Ctrl+End keys updated.

## [0.6.1]

### Added
- Initial version.
