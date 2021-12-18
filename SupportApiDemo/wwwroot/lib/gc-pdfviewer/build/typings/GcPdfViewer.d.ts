/// <reference path="vendor/i18next.d.ts" />
//@ts-ignore
import { i18n } from 'i18next';
//@ts-ignore
import { ViewerStatus, ViewMode, Toolbar, ToolbarLayout, ZoomMode } from '@grapecity/viewer-core';
import PdfReportPlugin, { ErrorEventArgs, BeforeOpenEventArgs, AfterOpenEventArgs } from './plugin';
import { SignToolSettings, ViewerOptions } from './ViewerOptions';
/// <reference path="vendor/react/react.d.ts" />
//@ts-ignore
import React from 'react';
/// <reference path="vendor/react/react-dom.d.ts" />
//@ts-ignore
import * as ReactDOM from 'react-dom';
//@ts-ignore
import { ReportViewer, LoadResult, EventFan } from '@grapecity/viewer-core';
import { ISupportApi, OpenDocumentInfo } from './SupportApi/ISupportApi';
import { PdfToolbarLayout } from './PdfToolbarLayout';
import { AnnotationBase, AnnotationTypeCode, CopyBufferData, WidgetAnnotation } from './Annotations/AnnotationTypes';
import { EditMode, LayoutMode } from './Annotations/types';
import { GcSelectionPoint } from './Models/GcMeasurementTypes';
import { GcRightSidebarState } from './RightSidebar/types';
import { GcRightSidebar } from './RightSidebar/GcRightSidebar';
import { LogLevel, ModificationsState } from './SharedDocuments/types';
import { SharedRef } from './Utils/SharedRef';
import { GcPdfSearcher } from './Search/GcPdfSearcher';
//@ts-ignore
import { PanelHandle } from '@grapecity/viewer-core/types/api/PluginModel';
import { DataStorage } from './DataStorage/DataStorage';
import { GcMeasurement } from './Utils/GcMeasurement';
import { SignToolStorage } from './SignTool/SignToolStorage';
import { LeftSidebar } from './LeftSidebar';
import { DocumentSecuritySummary } from './Security/DocumentSecuritySummary';
import { DocumentInformation } from './Properties/DocumentInformation';
import { OptionalContentConfig, ViewerFeatureName } from './Models/ViewerTypes';
import { AddStampDropdown } from './Toolbar/controls/AddStampDropdown';
export declare class GcPdfViewer extends ReportViewer {
    private static _i18n;
    static _instanceCounter: number;
    private _disposed;
    private _plugin;
    private _isUpdating;
    private _toolbarLayout;
    private _viewerInstanceId;
    readonly in17n: i18n;
    private _fileStorage;
    addStampDropdown: AddStampDropdown | null;
    constructor(element: HTMLElement | string, options?: Partial<ViewerOptions>);
    get instanceId(): string;
    set instanceId(id: string);
    get sharedRef(): SharedRef;
    get dataLoader(): import("./Core/GcPdfViewerDataLoader").GcPdfViewerDataLoader;
    get eventBus(): any;
    get serviceProvider(): any;
    static LicenseKey: string;
    get canEditDocument(): boolean;
    get currentUserName(): string;
    set currentUserName(userName: string);
    get disableFeaturesHash(): {
        [key in ViewerFeatureName]?: boolean;
    };
    get editMode(): EditMode;
    set editMode(mode: EditMode);
    get fileData(): Uint8Array | null;
    get fileName(): string;
    get fileUrl(): string;
    get leftSidebar(): LeftSidebar;
    get storage(): DataStorage;
    get signToolStorage(): SignToolStorage;
    get hasChanges(): boolean;
    get hasCopyData(): boolean;
    get hasDocument(): boolean;
    get hideAnnotations(): boolean;
    set hideAnnotations(hide: boolean);
    get layoutMode(): LayoutMode;
    set layoutMode(mode: LayoutMode);
    get logLevel(): LogLevel;
    set logLevel(logLvel: LogLevel);
    get modificationsState(): ModificationsState;
    get optionalContentConfig(): Promise<OptionalContentConfig>;
    get structureTree(): Promise<any[] | null>;
    get options(): Partial<ViewerOptions>;
    set options(options: Partial<ViewerOptions>);
    getType(typeName: string): typeof React | typeof AnnotationBase | typeof ReactDOM | typeof GcPdfViewer | typeof PdfReportPlugin | typeof AnnotationTypeCode | typeof EditMode | typeof ViewerStatus | typeof ViewerOptions | typeof LayoutMode | typeof Toolbar | typeof ViewMode | typeof ZoomMode | typeof GcMeasurement | typeof GcPdfSearcher | typeof GcRightSidebar | null;
    get plugin(): PdfReportPlugin;
    get rotation(): number;
    set rotation(degrees: number);
    get searcher(): GcPdfSearcher;
    get supportApi(): ISupportApi | null;
    get toolbarLayout(): PdfToolbarLayout;
    set toolbarLayout(toolbarLayout: PdfToolbarLayout);
    get version(): string;
    get hasReplyTool(): boolean;
    get rightSidebar(): GcRightSidebar;
    get fingerprint(): string;
    static get i18n(): i18n;
    get annotations(): Promise<{
        pageIndex: number;
        annotations: AnnotationBase[];
    }[]>;
    get beforeUnloadConfirmation(): boolean;
    set beforeUnloadConfirmation(enable: boolean);
    get hasForm(): boolean;
    get hasPersistentConnection(): boolean;
    showFormFiller(): void;
    showSignTool(preferredSettings?: SignToolSettings): void;
    resetChanges(): Promise<void>;
    reload(keepUndoState?: boolean): Promise<void>;
    get hasUndoChanges(): boolean;
    get hasRedoChanges(): boolean;
    get undoIndex(): number;
    get undoCount(): number;
    get pageIndex(): number;
    set pageIndex(val: number);
    get pageCount(): number;
    get isDocumentShared(): boolean;
    set zoomValue(val: number);
    get zoomValue(): number;
    set zoomMode(val: ZoomMode);
    get zoomMode(): ZoomMode;
    get onError(): EventFan<ErrorEventArgs>;
    get onBeforeOpen(): EventFan<BeforeOpenEventArgs>;
    get onAfterOpen(): EventFan<AfterOpenEventArgs>;
    setPageTabs(pageIndex: number, tabs: "S" | "R" | "C" | undefined): void;
    getPageTabs(pageIndex: number): "S" | "R" | "C" | undefined;
    setPageRotation(pageIndex: number, rotation: number): Promise<boolean>;
    getPageRotation(pageIndex: number): number;
    dispose(): void;
    logError(method: string, message: string): void;
    logDebug(method: string, message: string): void;
    _openingParams?: {
        file: string;
        promise: Promise<any>;
    };
    open(file: any): Promise<LoadResult>;
    pdfUrlToFileName(url: string): string;
    openLocalFile(): any;
    newDocument(params?: {
        fileName?: string;
        confirm?: boolean;
    } | string): Promise<LoadResult | null>;
    newPage(params?: {
        width?: number;
        height?: number;
        pageIndex?: number;
    }): Promise<void>;
    deletePage(pageIndex?: number): Promise<void>;
    print(): void;
    download(fileName?: string): void;
    save(fileName?: string): Promise<boolean>;
    saveChanges(): Promise<boolean>;
    submitForm(submitUrl: string): void;
    validateForm(validator?: (fieldValue: string | string[], field: WidgetAnnotation) => boolean | string, silent?: boolean, ignoreValidationAttrs?: boolean): string | boolean;
    getDocumentSecurity(): Promise<DocumentSecuritySummary>;
    getDocumentInformation(): Promise<DocumentInformation>;
    goToPageNumber(pageNumber: number): void;
    goToFirstPage(): void;
    goToPrevPage(): void;
    goToNextPage(): void;
    goToLastPage(): void;
    scrollPageIntoView(params: {
        pageNumber: number;
        destArray?: any[];
        allowNegativeOffset?: boolean;
    }): void;
    loadAndScrollPageIntoView(pageIndex: number, destArray?: any[]): Promise<boolean>;
    setTheme(theme?: string): void;
    execCutAction(buffer?: CopyBufferData): Promise<boolean>;
    execCopyAction(buffer?: CopyBufferData): Promise<boolean>;
    execDeleteAction(buffer?: CopyBufferData): Promise<boolean>;
    execPasteAction(point?: GcSelectionPoint): Promise<boolean>;
    loadDocumentList(documentListUrl?: string): void;
    openPanel(panelHandleOrId: PanelHandle | string): void;
    closePanel(panelHandleOrId?: PanelHandle | string): void;
    addDefaultPanels(): PanelHandle[];
    addDocumentListPanel(documentListUrl?: string): PanelHandle;
    addSharedDocumentsPanel(): PanelHandle;
    addThumbnailsPanel(): PanelHandle;
    addSearchPanel(): PanelHandle;
    addAnnotationEditorPanel(): PanelHandle;
    addArticlesPanel(): PanelHandle;
    addAttachmentsPanel(): PanelHandle;
    addOutlinePanel(): PanelHandle;
    addStructureTreePanel(): PanelHandle;
    addLayersPanel(): PanelHandle;
    addFormEditorPanel(): PanelHandle;
    addReplyTool(sidebarState?: GcRightSidebarState): void;
    cloneAnnotation(annotation: AnnotationBase): AnnotationBase;
    getSelectedText(): string;
    pushModificationsState(modificationsState: ModificationsState): void;
    toViewPortPoint(point: GcSelectionPoint): GcSelectionPoint;
    addStickyNote(position: GcSelectionPoint): void;
    changeOriginToBottom(pageIndex: any, y: any): number;
    changeOriginToTop(pageIndex: any, y: any): number;
    changeOrigin(pageIndex: number, y: number, srcOrigin: 'TopLeft' | 'BottomLeft', destOrigin: 'TopLeft' | 'BottomLeft'): number;
    changeBoundsOrigin(pageIndex: number, bounds: number[], srcOrigin: 'TopLeft' | 'BottomLeft', destOrigin: 'TopLeft' | 'BottomLeft'): number[];
    getViewPort(pageIndex: number): {
        viewBox: number[];
        width: number;
        height: number;
        scale: number;
        rotation: number;
    };
    getPageSize(pageIndex: number, includeScale?: boolean): {
        width: number;
        height: number;
    };
    setPageSize(pageIndex: number, size: {
        width: number;
        height: number;
    }): Promise<boolean>;
    addAnnotation(pageIndex: number, annotation: AnnotationBase): Promise<{
        pageIndex: number;
        annotation: AnnotationBase;
    }>;
    lockAnnotation(id: string | AnnotationBase): Promise<AnnotationBase | null>;
    unlockAnnotation(id: string | AnnotationBase): Promise<AnnotationBase | null>;
    addSignature(imageData: Uint8Array | null, args: {
        fileId: string;
        pageIndex: number;
        rect: number[];
        select?: boolean;
        subject?: string;
        fileName?: string;
        convertToContent?: boolean;
    }): Promise<{
        pageIndex: number;
        annotation: AnnotationBase;
    }>;
    addStamp(imageData: Uint8Array | null, args: {
        fileId: string;
        pageIndex: number;
        rect: number[];
        select?: boolean;
        subject?: string;
        fileName?: string;
        convertToContent?: boolean;
    }): Promise<{
        pageIndex: number;
        annotation: AnnotationBase;
    }>;
    loadSharedDocuments(): void;
    openSharedDocument(sharedDocumentId: string): Promise<OpenDocumentInfo | null>;
    updateAnnotation(pageIndex: number, annotation: AnnotationBase): Promise<{
        pageIndex: number;
        annotation: AnnotationBase;
    }>;
    updateAnnotations(pageIndex: number, annotations: AnnotationBase | AnnotationBase[]): Promise<{
        pageIndex: number;
        annotations: AnnotationBase[];
    }>;
    updateRadioGroupValue(fieldName: string, newValue: string, skipPageRefresh?: boolean): Promise<boolean>;
    removeAnnotation(pageIndex: number, annotationId: string): Promise<boolean>;
    findAnnotation(findString: string | number, findParams?: {
        findField?: 'id' | 'title' | 'contents' | 'fieldName' | string;
        pageNumberConstraint?: number;
        findAll?: boolean;
    }): Promise<{
        pageNumber: number;
        annotation: AnnotationBase;
    }[]>;
    selectAnnotation(pageIndex: number | string, annotation?: AnnotationBase | string): Promise<boolean>;
    unselectAnnotation(): any;
    scrollAnnotationIntoView(pageIndex: number, annotation: AnnotationBase): any;
    undoChanges(): void;
    redoChanges(): void;
    repaint(indicesToRepaint?: number[]): void;
    applyOptions(): void;
    applyToolbarLayout(): void;
    getAnnotationPageIndex(annotation: string | AnnotationBase): number | null;
    getPageLocation(pageIndex: number): {
        x: number;
        y: number;
    };
    setAnnotationBounds(annotationId: string, bounds: {
        x: number | undefined;
        y: number | undefined;
        w: number | undefined;
        h: number | undefined;
    }, origin?: 'TopLeft' | 'BottomLeft', select?: boolean): Promise<void>;
    getRenderedAnnotationBounds(id: string, windowRelative?: boolean): {
        x: number;
        y: number;
        w: number;
        h: number;
    };
    globalPointToPagePoint(mousePosition: GcSelectionPoint): GcSelectionPoint;
    enablePdfToolButtons(buttons?: string[] | "all" | 'none'): void;
    updateLayout(layout: ToolbarLayout): void;
    showMessage(message: string, details?: string, severity?: "error" | "warn" | "info" | "debug"): void;
    clearMessages(): void;
    goBack(): void;
    goForward(): void;
    beginUpdate(): void;
    endUpdate(): void;
    get isUpdating(): boolean;
    private static _init_i18n;
}
export default GcPdfViewer;
