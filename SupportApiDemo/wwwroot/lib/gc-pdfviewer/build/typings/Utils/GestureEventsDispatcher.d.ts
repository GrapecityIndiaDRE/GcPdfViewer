export declare type DispatcherMode = 'None' | 'Viewer' | 'Layer';
export declare type PointerEventsListener = {
    start: (event: TouchEvent | MouseEvent) => boolean;
    move: (event: TouchEvent | MouseEvent) => boolean;
    end: (event: TouchEvent | MouseEvent) => boolean;
    multipleStart?: (event: TouchEvent) => void;
};
export declare class GestureEventsDispatcher {
    _active: boolean;
    private _pointerListener?;
    _hostElement?: HTMLElement;
    _onGestureStartHandler: any;
    _onGestureStopHandler: any;
    _onGestureMoveHandler: any;
    _onMouseDownHandler: any;
    _onMouseMoveHandler: any;
    _onMouseUpHandler: any;
    _registeredElements: any;
    _docViewerId: string;
    private _lastEventKey;
    constructor(docViewerId: string);
    initialize(hostElement: HTMLElement): void;
    static instance(docViewerId: string): GestureEventsDispatcher;
    setPointerEvents(dispatcherMode: DispatcherMode, pointerListener: PointerEventsListener): void;
    clearPointerEvents(dispatcherMode?: DispatcherMode): void;
    registerForEvents(key: string, element: Document | HTMLElement): void;
    unregisterForEvents(key: string): void;
    private _registerEvents;
    private _unregisterEvents;
    private _onGestureStart;
    private _onGestureMove;
    private _onGestureStop;
    private _onMouseDown;
    private _onMouseMove;
    private _onMouseUp;
    _isEventDispatched(e: TouchEvent | MouseEvent): boolean;
    notifyTouchEnd(event: TouchEvent): void;
}
