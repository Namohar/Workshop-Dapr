import { AzureFunction, Context } from "@azure/functions"

const cosmosDBTrigger: AzureFunction = async function (context: Context, documents: any[]): Promise<void> {
    if (!!documents && documents.length > 0) {
        documents.forEach(element => {
            context.log("Element itself: ", element)
            context.log('Element Id: ', element.id);    
            context.log('Element Name: ', element.name);  
            
            context.bindings.history = JSON.stringify({
                documentId: element.id,
                name: element.name,
                timestamp: element._ts                
              });
        
              
        });
        context.done();
    }
}

export default cosmosDBTrigger;
