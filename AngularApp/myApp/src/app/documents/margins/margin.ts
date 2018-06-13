import { TextClass } from "../texts/text";

export class Margin{
    Id: string;
    OwnStyleClass: string;
    Texts: Array<TextClass>;
    MarginAlign: number;
    DocumentId: string;
  
    constructor(id : string, ownStyleClass : string, marginAlign : number, texts : Array<TextClass>, documentId : string){

            this.Id = id;
            this.OwnStyleClass = ownStyleClass;
            this.MarginAlign = marginAlign;
            this.Texts = texts;
            this.DocumentId = documentId;
    }
  }