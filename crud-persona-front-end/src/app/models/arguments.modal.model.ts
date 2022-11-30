export interface ArgumentsModal<T> {
    item: T,
    itemList?: T[],
    add: boolean,
    edit: boolean;
    view: boolean;
}