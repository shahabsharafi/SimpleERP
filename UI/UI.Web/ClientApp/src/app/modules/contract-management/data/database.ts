import Dexie from 'dexie';
import { IContractModel } from '../models';


class ContractManagementDatabase extends Dexie {
    // Declare implicit table properties.
    // (just to inform Typescript. Instanciated by Dexie in stores() method)
    contracts: Dexie.Table<IContractModel, number>; // number = type of the primkey
    //...other tables goes here...

    constructor () {
        super("ContractManagementDatabase");
        this.version(1).stores({
            contracts: '++id, first, last',
            //...other tables goes here...
        });
        // The following line is needed if your typescript
        // is compiled using babel instead of tsc:
        this.contracts = this.table("contracts");
    }
}

