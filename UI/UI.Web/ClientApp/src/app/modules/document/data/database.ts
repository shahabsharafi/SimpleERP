import Dexie from 'dexie';
import { IDocumentModel } from '../models';


class DocumentDatabase extends Dexie {
    // Declare implicit table properties.
    // (just to inform Typescript. Instanciated by Dexie in stores() method)
    documents: Dexie.Table<IDocumentModel, number>; // number = type of the primkey
    //...other tables goes here...

    constructor () {
      super("DocumentDatabase");
        this.version(1).stores({
            contracts: '++id, no',
            //...other tables goes here...
        });
        // The following line is needed if your typescript
        // is compiled using babel instead of tsc:
      this.documents = this.table("documents");
    }
}

