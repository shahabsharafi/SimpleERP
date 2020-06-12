/*******************************************************/
$ docker 
https://virgool.io/DockerMe/%DA%AF%D8%B1%DB%8C%D8%B2-%D8%A7%D8%B2-%D8%AA%D8%AD%D8%B1%DB%8C%D9%85-%D8%AF%D8%A7%DA%A9%D8%B1-%D8%A8%D8%A7-%DA%86%D9%86%D8%AF-%D8%B1%D9%88%D8%B4-z6czoxibqnyk
DNS for docker 
https://shecan.ir/

/*********************************************************/
use from command: "dotnet new react -o new_react_project"
or : "npx create-react-app my-app --typescript" for typescript supp/ and install nodejs

install:
npm install -g npx

data grid:
https://www.npmjs.com/package/@types/react-data-grid
npm install --save @types/react-data-grid

https://www.npmjs.com/package/@types/reactstrap
npm install --save @types/reactstrap

https://www.npmjs.com/package/@types/react-router-dom
npm install --save @types/react-router-dom

https://jaredpalmer.com/formik/docs/overview
npm install formik --save

/*******************************************************/
RabbitMQ
https://www.rabbitmq.com/
$ docker pull rabbitmq:3-management
$ docker run -d --hostname rabbitmq --name some-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management

/*******************************************************/
Memurai
Redis-compatible cache and datastore for Windows
https://www.memurai.com/

OR 
/*******************************************************/
Using Redis by docker
https://koukia.ca/installing-redis-on-windows-using-docker-containers-7737d2ebc25e
$ docker pull redis
$ docker run --name my-redis -d -p 6390:6390 redis --port 6390
$ docker ps
$ docker logs my-redis
$ docker exec -it my-redis sh
# redis-cli
127.0.0.1:6379> set yourkey "your test value"
OK
127.0.0.1:6379> get yourkey "your test value"
127.0.0.1:6379>

Nuget package for redis
Microsoft.Extensions.Caching.Redis

/*******************************************************/
Using ElasticSearch
https://www.elastic.co/what-is/elasticsearch

Persian resource about ELK Stack
https://www.apk.co.ir/kb/what-is-the-elk-stack/

Installing ElasticSearch by docker
https://hub.docker.com/_/elasticsearch

$ docker pull elasticsearch:tag

Running in developmentmode: 
Create user defined network (useful for connecting to other services attached to the same network (e.g. Kibana)):
$ docker network create somenetwork

Run Elasticsearch:
$ docker run -d --name elasticsearch --net somenetwork -p 9200:9200 -p 9300:9300 -e "discovery.type=single-node" elasticsearch:tag

running in production mode:
$ docker run -p 9200:9200 -p 9300:9300 -e "discovery.type=single-node" elasticsearch:7.5.1

query for get all index:
http://localhost:9200/_aliases?pretty=true

query for get all auditlog records:
http://localhost:9200/index-contract-management/_search?pretty=true&q=messageTemplate:*audit*

for test:
curl -X POST \
  http://127.0.0.1:9200/_msearch \
  -H 'cache-control: no-cache' \
  -H 'content-type: application/x-ndjson' \
  -d '{"index":"script","type":"test"}3
{"query":{"match_all":{}}}
'

/*******************************************************/
Install kibana docker
https://www.elastic.co/guide/en/kibana/current/docker.html

$ docker pull kibana:7.5.1

Choose network from list for example (somenetwork)
$ docker network ls

Run kibana
$ docker run -d --name kibana --net somenetwork -p 5601:5601 kibana:7.5.1

Kibana service url: 
http://localhost:5601

/*******************************************************/
Dexie.js is a minimalistic wrapper for IndexedDB 
https://dexie.org/docs/Tutorial/Consuming-dexie-as-a-module

Use TypeScript in Dexie.js
https://dexie.org/docs/Typescript

npm install dexie --save

class MyAppDatabase extends Dexie {
    // Declare implicit table properties.
    // (just to inform Typescript. Instanciated by Dexie in stores() method)
    contacts: Dexie.Table<IContact, number>; // number = type of the primkey
    //...other tables goes here...

    constructor () {
        super("MyAppDatabase");
        this.version(1).stores({
            contacts: '++id, first, last',
            //...other tables goes here...
        });
        // The following line is needed if your typescript
        // is compiled using babel instead of tsc:
        this.contacts = this.table("contacts");
    }
}

interface IContact {
    id?: number,
    first: string,
    last: string
}

/*******************************************************/
Downloading docker image for transfer to non-internet-connected machine

You can pull the image on a computer that have access to the internet.
$ docker pull ubuntu

Then you can save this image to a file
$ docker save -o ubuntu_image.docker ubuntu

Transfer the file on the offline computer (USB/CD/whatever) and load the image from the file:
$ docker load -i ubuntu_image.docker

/*******************************************************/
bpmn-js Modeler
implement a modeler for BPMN 2.0 process diagrams

"bpmn-js": "^6.2.1",
"bpmn-js-properties-panel": "^0.32.0",
"camunda-bpmn-moddle": "^4.0.1",
"diagram-js": "^5.0.0",

/*******************************************************/
remove container that they are not up
$docker container rm $(docker container ls -a --filter status=exited --filter status=created -aq)

/******************************************************/
MySql
https://medium.com/@havruk0stas/how-to-properly-use-ef-core-in-asp-net-core-with-mysql-database-a75f56c97318
https://medium.com/@Likhitd/asp-net-core-and-mysql-with-docker-part-3-e3827e006e3

start a simple instance
$ docker run --name db -p 3306:3306 -e MYSQL_ROOT_PASSWORD=... -d mysql
in appsetting
"DefaultConnection": "server=localhost;port=3306;database=...;user=...;password=..."",

connect to mysql in command line
$ docker exec -it db mysql -u root -p
$ Enter password: my-secret-pw
$ mysql> show databases;

Install dotnet ef tool 
$ dotnet tool install --global dotnet-ef --version 3.0.0

management tool for mysql
$ docker pull phpmyadmin/phpmyadmin
$ docker run --name myadmin -d --link db:db -p 8080:80 phpmyadmin/phpmyadmin

migration
$ dotnet ef migrations add initial -o ./Infrastructure/Migrations
$ dotnet ef database update

OR

https://www.entityframeworktutorial.net/code-first/code-based-migration-in-code-first.aspx
enable-migrations
add-migration initial -OutVariable ./Infrastructure/Migrations
Update-Database

Tutorial: Using the migrations feature - ASP.NET MVC with EF Core
https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/migrations?view=aspnetcore-3.0#introduction-to-migrations

Install-Package Pomelo.EntityFrameworkCore.MySql

services.AddDbContext<ApplicationDbContext>(options => options
    .UseMySQL(Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("TadkarSoft.Tadkar.EPMS.ContractManagement.API")
));

connecting to mysql
https://medium.com/@Likhitd/asp-net-core-and-mysql-with-docker-part-3-e3827e006e3


/*************************************************************/
dockerize project

https://medium.com/@Likhitd/asp-net-core-and-mysql-with-docker-part-1-b7ef538ecd8e

/************************************************************/
EPERM: Operation not permitted - NPM Angular 7 on IIS with backend .Net Core 2.1
https://stackoverflow.com/questions/56234007/eperm-operation-not-permitted-npm-angular-7-on-iis-with-backend-net-core-2-1
What helped is 
creating "npm" folder in C:\Windows\System32\config\systemprofile\AppData\Roaming 
and assigning read permission for the folder to IIS_USR group.