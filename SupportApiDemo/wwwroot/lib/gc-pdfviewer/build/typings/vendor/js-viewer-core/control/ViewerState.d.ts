import { PluginModel, ReportViewerCmd, ReportViewerCommandStatus, DocumentMoniker } from '../api';
import { DocumentViewer } from "../components";
import { SearchResult } from '../features/search';
import { CancellationToken } from './CancellationToken';
import { LoadResult } from './SessionState';
export declare class ViewerState {
    readonly raiseChangedEvent: (model: DocumentViewer.Model) => void;
    readonly raiseOpenDocumentEvent: (document: PluginModel.IDocument | null) => void;
    readonly raiseOpenDocumentViewEvent: (document: PluginModel.IDocumentView | null) => void;
    readonly onDocumentProgress: (view: PluginModel.ProgressMessage) => void;
    readonly errorSink: PluginModel.IReportError;
    readonly viewerState: PluginModel.IStateBinder<DocumentViewer.Model>;
    constructor(raiseChangedEvent: (model: DocumentViewer.Model) => void, raiseOpenDocumentEvent: (document: PluginModel.IDocument | null) => void, raiseOpenDocumentViewEvent: (document: PluginModel.IDocumentView | null) => void, onDocumentProgress: (view: PluginModel.ProgressMessage) => void, errorSink: PluginModel.IReportError, viewerState: PluginModel.IStateBinder<DocumentViewer.Model>);
    private _plugin;
    private _session;
    private readonly _settingsStore;
    private readonly _viewerStore;
    readonly viewState: DocumentViewer.Model;
    readonly viewSettings: DocumentViewer.ViewSettings;
    readonly isDocumentOpened: boolean;
    readonly documentView: PluginModel.IDocumentView | null;
    updateUi: (msg: DocumentViewer.SettingsMsg) => void;
    toggleNarrowScreen: (isNarrow?: boolean | undefined) => void;
    highlight: (result: SearchResult | null) => Promise<void>;
    setPlugin(plugin: PluginModel.IPluginModule<PluginModel.ViewerEvent, any>): void;
    private onChangeDocument;
    private onChangeDocumentView;
    private getContext;
    resolveAction: (event: MouseEvent) => any;
    processAction: (action: PluginModel.ViewerAction) => boolean;
    private processEvent;
    handleViewerCmd: (cmd: ReportViewerCmd) => void;
    readonly commandStatus: ReportViewerCommandStatus;
    resetDocument: () => Promise<void>;
    load: (doc: DocumentMoniker, parentToken?: CancellationToken | undefined) => Promise<LoadResult>;
    open: (doc: DocumentMoniker) => Promise<LoadResult>;
    private processLoadResult;
    private drillDocument;
    private cancelSession;
    private startSession;
    private readonly _history;
    private _historyPosition;
    pushEvent: (event: PluginModel.ViewerEvent) => void;
    private historyGoBack;
    private historyGoParent;
    private historyGoForward;
    private historyReset;
    private historyResetNavigation;
}
