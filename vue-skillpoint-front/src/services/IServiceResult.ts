export interface IServiceResult<TData> {
    status: number;
    data?: TData;
    errorMsg?: string;
}
