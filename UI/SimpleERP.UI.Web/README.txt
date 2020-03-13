#Debug apps in a local Docker container
#https://docs.microsoft.com/en-us/visualstudio/containers/edit-and-refresh?view=vs-2019

#Docker for Windows
#https://docs.docker.com/docker-for-windows/install/

#https://angular.io/guide/setup-local
#To install the CLI using npm, open a terminal/console window and enter the following command:
npm install -g @angular/cli
#Run the CLI command ng new and provide the name my-app, as shown here:
ng new my-app
#Launch the server by using the CLI command ng serve, with the --open option.
cd my-app
ng serve --open


#OR

#https://docs.microsoft.com/en-us/aspnet/core/client-side/spa/angular?view=aspnetcore-3.0&tabs=visual-studio
dotnet new angular -o my-new-app
cd my-new-app
#install packages
cd ClientApp
npm install --save <package_name>
#In a command prompt, switch to the ClientApp subdirectory, and launch the Angular CLI development server:
cd ClientApp
npm start

#Libraries

//#https://www.primefaces.org/primeng/#/setup
//#PrimeNG is available at npm, if you have an existing application run the following command to download it to your project.
//npm install primeng --save
//npm install primeicons --save

#https://www.primefaces.org/primeng/#/multiselect
#CDK
#Virtual Scrolling enabled MultiSelect depends on @angular/cdk's ScrollingModule so begin with installing CDK if not already installed.
npm install @angular/cdk --save

#https://www.ag-grid.com/ag-grid-typescript-webpack-2/
#ag-Grid is a feature-rich data grid built for Angular. Integrate seamlessly with Angular 2,4,5,6,7
npm i --save @ag-grid-community/all-modules
npm i --save @ag-grid-enterprise/all-modules
npm install # in certain circumstances npm will perform an "auto prune". This step ensures all expected dependencies are present

#https://angular2-tree.readme.io/docs/
#Angular Tree Component
#npm install --save angular-tree-component
#RTL support
#Keyboard navigation
#Drag & Drop
#Virtual Scroll to support large trees
#Checkboxes
#Async children load
#Expand / collapse / select nodes
#Events: activate, collapse, expand, focus, etc.
#Custom node template (string or component)
#Custom loading component (for async data)
#Custom children / name attributes
#API
#Very basic customizable CSS

#Tools

#https://www.erlang.org/downloads
#you need this for install rabbitmql
#Erlang is a programming language originally developed at the Ericsson Computer Science Laboratory


#https://www.rabbitmq.com/download.html
#RabbitMQ is the most widely deployed open source message broker

#bootstrap rtl suport
npm i bootstrap-v4-rtl

#angular bootstrap suport
npm install --save @ng-bootstrap/ng-bootstrap

#https://github.com/fingerpich/jalali-moment/blob/master/package.json
#Jalali Moment
#install module
npm install jalali-moment -S
#import modele
import * as moment from 'jalali-moment';
#definition of pipe
@Pipe({
  name: 'jalali'
})
export class JalaliPipe implements PipeTransform {
  transform(value: any, args?: any): any {
    let MomentDate = moment(value, 'YYYY/MM/DD');
    return MomentDate.locale('fa').format('YYYY/M/D');
  }
}
#usage of pipe
<div>{{ loadedData.date | jalali }}</div>