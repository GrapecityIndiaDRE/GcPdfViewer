export declare function createPromiseCapability(): {
    promise: Promise<any>;
    resolve: Function;
    reject: Function;
};
export declare function createPromiseCapabilityWithTimeout(rejectTimeout?: number): {
    promise: Promise<any>;
    resolve: Function;
    reject: Function;
};
