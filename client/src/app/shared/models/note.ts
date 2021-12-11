export interface INote{
  id: number;
  title?: string;
  content?: string;
  startIndex?: number;
  endIndex?: number;
  materialId: number;
  creationDate?: Date;
}
