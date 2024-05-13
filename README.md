**Documentação – MotoRental**


Backend desenvolvido com o objetivo de criar uma aplicação para gerenciar aluguel de motos e entregadores.
A documentação do Swagger ficou dividida em 3 tópicos.

**DeliveryDriver**
Executa ações voltadas ao condutor. Faz seu devido cadastro, atualiza a imagem da cnh e consulta valores prévios de aluguel.

**Moto**
Crud do veículo e pesquisa pela placa.
Vale ressaltar na definição, caso o ano do veiculo seja 2024 juntamente com a criação do cadastro, no campo modelo, é cadasatrado também esta sinalização, para avaliações posteriores.


**Rental**
Executa ações do aluguel, atrelando o condutor ao veículo.
Softwares utilizados: 
1.	PostgreSQL
2.	RabbitMQ
3.	Visual studio 2022

**Passos para execução do projeto (Baixe o pdf para visualizar as imagens)**
1.	Localmente, faça a instalação dos softwares utilizados.
2.	RabbitMQ – após sua instalação, caso necessário habilite o plugin de gerenciamento do RabbitMQ através do comando rabbitmq-plugins enable rabbitmq_management em seu diretório de instalação, ou via ferramenta 
3.	Em seu diretório c: crie a pasta MotoRentalUploads, para armazenamento de arquivos png e bmp
4.	Com o projeto aberto no visual studio -> botão direito em solutions ->propriedades e configurar conforme a figura-3
5.	Se necessário, configurar a Exchange do rabbitMQ conforma figura-4
6.	Explore as chamadas da api

