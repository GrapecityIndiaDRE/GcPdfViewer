# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.1.9] - 12-Nov-2021
### Fixed
- Checkbox value "Off" not saved.

## [2.1.8] - 01-Nov-2021
### Fixed
- Fixed issue with annotationName and contents properties for LinkAnnotation. (DOC-3680)

## [2.1.7] - 29-Oct-2021
### Fixed
- Reset buttons not saved for GcPdfViewer v3.0.*.

## [2.1.6] - 01-Oct-2021
### Fixed
- The request type for version / lastError requests from the viewer has been changed to POST. (DOC-3656)

## [2.1.5] - 29-Sep-2021
### Fixed
- Fixed problem with "Void" stamp image localization.

## [2.1.4] - 22-Sep-2021
### Added
- Added ability to localize stamp images and strings.
  Localization strings location:
    SupportApi\Localization\StringsTable.resx
  Localization images location:
    SupportApi\Localization\ImagesTable.resx
  Here's an example how to change the localization language:
```csharp
  SupportApi.Localization.Localizer.Culture = CultureInfo.GetCultureInfo("ja-JP");
```

## [2.1.3] - 15-Sep-2021
### Fixed
- Incorrect annotation name after saving the document and re-loading for code added annotation. (DOC-3619)

## [2.1.2] - 29-Jul-2021
### Fixed
- Error saving document after adding all predefined stamps to canvas. (DOC-3358)

## [2.1.1] - 23-Jul-2021
### Fixed
- On changing the Required field to true, the field name gets changed and only last part of hierarchy fields. (DOC-3449)

## [2.0.8] - 20-Jul-2021
### Fixed
- The opacity effect looks different from Adobe. (DOC-3359)

## [2.0.7] - 29-Jun-2021
### Added
- Predefined stamps support. (DOC-3130)

## [2.0.6] - 09-Jun-2021
### Added
- Added ability to modify page's fields Tab order using the Form Editor. (DOC-3168)
- Added ability to modify page size. (DOC-2751)

## [2.0.5] - 25-May-2021
### Fixed
- Fixed null reference exception in some rare cases when applying client changes to annotations.

## [2.0.4] - 28-Apr-2021
### Added
- Added font name support for text fields and free text annotations. (DOC-3132)

## [2.0.3] - 20-Apr-2021
### Fixed
- There's no file for FileAttachment annotation after save and reload. (DOC-3109)

## [2.0.2] - 05-Apr-2021
### Added
- Support for the opacity property for annotations. (DOC-2954)
### Fixed
- [Stamp annotations] fixed image stretching issue.
- [Link Annotation] fixed problem with FitR destination.

## [2.0.1] - 10-Mar-2021
### Added
- Added support for Link Annotation. (DOC-2270)
### Fixed
- Required property is missing for TextField form after saving and reloading. (DOC-2808)
- Missing contents property of the RedactAnnotation after save and reload. (DOC-2806)
- The sound annotation doesn't show the file size in Collaboration mode. (DOC-2416)
- The FileAttachment annotation doesn't show the file size in Collaboration. (DOC-2414)

## [1.2.10] - 05-Feb-2021
### Added
- Added support for GcPdfViewer v.4.1  

## [1.2.9] - 15-Jan-2021
### Fixed
- Rotation issue when saving a Pdf. (DOC-2681)

## [1.2.8] - 12-Dec-2020
### Fixed
- Fixed problem with saving file attachments.

## [1.2.7] - 04-Dec-2020
### Added
- Added localization strings for error messages, see ErrorMessages / ErrorMessages_Ja.

## [1.2.6] - 16-Nov-2020
### Fixed
- [Collaboration] Fixed problem with automatic client reconnection. (DOC-2571)

## [1.2.5] - 16-Nov-2020
### Fixed
- Fixed exception when SupportApi is used in self-hosted OWIN application.

## [1.2.4] - 10-Nov-2020
### Fixed
- [Collaboration] Fixed problem with large attachments (DOC-2416/DOC-2414)

## [1.2.3] - 04-Nov-2020
### Fixed
- [Collaboration] The users list who have access to document is incorrect after restart the server. (DOCXLS-3243)
- [Collaboration] Previous access information is displayed for new documents. (DOC-2402)
- [Collaboration] Insert blank page in Collaboration mode doesn't real-time co-authoring. (DOC-2410)

## [1.2.2] - 27-Oct-2020
### Added
- Support token-based authentication. (DOC-2432)
### Fixed
- non-checked RadioButton is not converted to content. (DOC-2376)

## [1.2.1] - 28-Sep-2020
### Added
- Added ability to convert annotations and fields to content. (DOC-1883)
- Collaboration mode feature. (DOC-1595)

## [1.1.26] - 02-Sep-2020
### Fixed
- "incorrect document structure change" after deleting and adding a new page. (DOC-2299)

## [1.1.25] - 24-Aug-2020
### Fixed
- Fixed exception "Failed to compare two elements in the array". (DOC-2272)
- Modified ink annotation retains old ink lists after saving document with SupportApi. (DOC-2283)

## [1.1.24] - 31-July-2020
### Fixed
- On applying redact for two part of scanned page, the saved page has no content. (DOC-2254)

## [1.1.23] - 11-July-2020
### Added
- RadioButtonField: added support for radiosInUnison property. (DOC-2205)

## [1.1.23] - 11-July-2020
### Added
- FreeTextAnnotation: Added a default value for the LineDashPattern property when the border style is set to dashed, but lineDashPattern is not sent from the client viewer.

## [1.1.22] - 30-June-2020
### Added
- Added support for markup annotation references, State and StateModel properties.

## [1.0.12] - 29-June-2020
### Fixed
- The fields order is changed after modifying the name of a radio button in form editor mode. (DOC-2081)
- Cannot save empty text alignment.

## [1.0.11] - 11-June-2020
### Fixed
- Cannot rename radio button field to an existing name to create a radio button group (case 2). (DOC-2025)

## [1.0.10] - 28-May-2020
### Added
- Added new properties: fileUrl, fileName. Sample usage in a controller derived from GcPdfViewerController:
```csharp
public override void OnDocumentModified(GcPdfDocumentLoader documentLoader) {
  string fileUrl = documentLoader.Info.documentOptions.fileUrl;
  string fileName = documentLoader.Info.documentOptions.fileName;
}
```

## [1.0.9] - 09-May-2020
### Fixed
- Incorrect file extension when downloading file using the "save" button in IE11. (DOC-2009)

## [1.0.8] - 08-May-2020
- FreeText annotation's text box size is incorrect when annotation
  updated using Annotation Editor in some cases. (DOC-1990)

## [1.0.7] - 05-May-2020
- All form controls get named with "_1" at end and can't remove it. (DOC-2001)

## [1.0.6] - 04-May-2020
### Fixed
- Adding a field with a duplicate name to a form shows an "Unknown Widget" when loading that form into the viewer. (DOC-1991)
- Deleting a form field could delete just the widget, leaving the field. (DOC-1991)
- Form field changes not saved when field owner is another field. (DOC-1997)
- Forecolor applies on border as well. (DOC-1963)
- Display error message when GcPdf is not licensed. (DOC-1818)

## [1.0.5] - 24-Apr-2020
### Added
- Added support for userData option (Arbitrary data associated with the viewer).
  Sample usage in a controller derived from GcPdfViewerController:
```csharp
  public override void OnDocumentModified(GcPdfDocumentLoader documentLoader) {
    object userData = documentLoader.Info.documentOptions.userData;
  }
```

## [1.0.4] - 22-Apr-2020
### Added
- Added the ability to open a PDF from an external URL.
### Fixed
- Fixed a problem when opening documents without meta information.
- Incorrect rendering of redact annotations in some PDFs. (DOC-1976)
- Fixed a problem with updating form fields tab order. (DOC-1982)

## [1.0.3] - 09-Apr-2020
### Fixed
- Viewer pops up an error message when opening a password-protected file. (DOC-1946)

## [1.0.2] - 02-Apr-2020
### Added
- Added support for WebForm applications.
- Display 'Document is too large' message when document exceeds request size allowed by HTTP server.
### Fixed
- Fixed a thread safety problem when SupportApi is used to open several documents at once.
- Text justification does not applied to a text field widget when modified document is saved and loaded into viewer again. (DOC-1873)
- Fixed problem with resolving Download action for Web Api 2 (Web Forms) routing.
- Fixed problem with converting line annotation's coordinate values for some specific cultures.

## [1.0.1] - 10-Mar-2020
### Added
- Added ability to edit basic Link annotation properties. (DOC-1847)
- Version number (Properties/AssemblyInfo.cs) (1.0.1).
- CHANGELOG.md (this file).

## [1.0.0] - 05-Mar-2020
### First public release.
