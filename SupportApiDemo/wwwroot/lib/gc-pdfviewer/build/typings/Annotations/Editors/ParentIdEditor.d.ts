/// <reference path="../../vendor/react/react.d.ts" />
//@ts-ignore
import { Component } from 'react';
//@ts-ignore
import { PropertyEditorProps } from '@grapecity/core-ui';
import { AnnotationTypeName } from './../AnnotationTypes';
/// <reference path="../../vendor/i18next.d.ts" />
//@ts-ignore
import { i18n } from 'i18next';
export declare type ParentIdEditorLocalization = {
    noneItem: {
        label: string;
        title: string;
    };
};
export declare type ParentIdEditorProps = PropertyEditorProps & ParentIdEditorLocalization & {
    subtypeConstraint?: AnnotationTypeName;
    in17n: i18n;
};
export declare class ParentIdEditor extends Component<ParentIdEditorProps, any> {
    render(): JSX.Element;
}
