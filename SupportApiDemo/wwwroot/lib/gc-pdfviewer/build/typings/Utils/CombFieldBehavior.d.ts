export declare class CombFieldAppearance {
    input: HTMLInputElement;
    combsCount: number;
    private combWidth;
    private container?;
    constructor(input: HTMLInputElement, combsCount: number, combWidth: number, container?: HTMLElement | undefined);
    private _createControls;
}
export declare class CombFieldBehavior {
    input: HTMLInputElement;
    combsCount: number;
    private styleReferenceElement;
    private onInputTabCallback?;
    private onInputChangedCallback?;
    private onInputBlurCallback?;
    static selectionBgColor: string;
    static selectionFgColor: string;
    private _combInputs;
    private _combInputsSelection;
    private _combInputsContainer;
    private _textAlign;
    private _focusedCombIndex;
    combInitColor: string;
    combInitBackgroundColor: string;
    private _readOnly;
    constructor(input: HTMLInputElement, combsCount: number, styleReferenceElement: HTMLElement, onInputTabCallback?: Function | undefined, onInputChangedCallback?: Function | undefined, onInputBlurCallback?: Function | undefined);
    initialize(): void;
    set readOnly(readOnly: boolean);
    get readOnly(): boolean;
    private _findCombWithValue;
    private _focusComb;
    private _extendCombSelections;
    get borderWidth(): number;
    updateCombsLayout(fieldWidth?: number): void;
    private _createControls;
    getSelectedText(): string;
    _setCombValues(value: string): void;
    _clearSelectedCombsWithShift(moveCaretIndex?: number): boolean;
    _updateUnderlyingInputValue(): void;
    _selectAllCombs(): void;
    _deselectAllCombs(): void;
    _onFocus(combInput: HTMLInputElement): void;
    _onBlur(): void;
    set textAlign(val: 'left' | 'center' | 'right' | undefined);
    get textAlign(): 'left' | 'center' | 'right' | undefined;
    _shiftCombValueIfNeeded(ind: number): boolean;
    _insertCharWithShift(curCombInput: HTMLInputElement, ch: string): number;
    _repaintCombSelections(): void;
}
