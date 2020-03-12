import Dexie from 'dexie';
import { IDocumentInfoModel } from '../models';


class DocumentDatabase extends Dexie {
    // Declare implicit table properties.
    // (just to inform Typescript. Instanciated by Dexie in stores() method)
    documentinfos: Dexie.Table<IDocumentInfoModel, number>; // number = type of the primkey
    //...other tables goes here...

    constructor () {
      super("DocumentInfoDatabase");
        this.version(1).stores({
            contractinfos: '++id, no',
            //...other tables goes here...
        });
        // The following line is needed if your typescript
        // is compiled using babel instead of tsc:
      this.documentinfos = this.table("documentinfos");
    }
}

