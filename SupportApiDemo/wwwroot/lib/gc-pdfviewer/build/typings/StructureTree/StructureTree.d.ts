/// <reference path="../vendor/react/react.d.ts" />
//@ts-ignore
import { Component } from 'react';
import GcPdfViewer from '..';
import { StructureTreeModel } from './types';
export declare type StructureTreeProps = {
    viewer: GcPdfViewer;
};
export declare class StructureTree extends Component<StructureTreeProps, StructureTreeModel> {
    private _mounted;
    componentDidMount(): void;
    componentWillUnmount(): void;
    private onItemClick;
    private toggleExpanded;
    private renderPageNode;
    render(): JSX.Element;
}
