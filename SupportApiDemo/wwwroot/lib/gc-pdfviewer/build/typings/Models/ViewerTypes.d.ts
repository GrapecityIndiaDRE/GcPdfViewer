export declare type OptionalContentConfig = {
    creator: null | string;
    name: null | string;
    getGroup(id: string): OptionalContentGroup;
    getGroups(): {
        [id: string]: OptionalContentGroup;
    };
    getOrder(): {
        name: string | null;
        order: string[];
    }[];
    isVisible(group: OptionalContentGroup): any;
    setVisibility(id: string, visible: boolean): any;
};
export declare type OptionalContentGroup = {
    id: string;
    name: string;
    type: "OCG" | string;
    visible: boolean;
};
export declare type StructTreeNode = {
    role: "Root" | string;
    children: StructTreeContent[];
};
export declare type StructTreeContent = {
    id: string;
    type: "content" | string;
    children: StructTreeContent[];
};
export declare type ViewerFeatureName = 'JavaScript' | 'AllAttachments' | 'FileAttachments' | 'SoundAttachments' | 'DragAndDrop' | 'SubmitForm' | 'Print';
export declare type StampCategory = {
    id: string;
    name: string;
    stampImages: string[];
    stampImageUrls?: string[];
    isDynamic: boolean;
    dpi: number;
};
