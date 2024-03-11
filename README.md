# Orchestratore Kubernetes
Un orchestratore che gestisce un applicazione in microservizi.
I microservizi avranno le seguenti funzionalità:
1. Modulo Web Server, questo mostrerà una pagina grafica che espone le variabili generate dai processi-
2. Modulo Ricevitore, questo ha il compito di prelevare i dati dei processi remoti e salvarli sul database.
3. Modulo RD, questo ha il compito di generare dei dati random alla richiesta del ricevitore.

## Infrastruttura
Nodo MASTER
Processi:
- Database Redis
- 3 x Ricevitori
- 1 x RD
- 1 x Web server

Nodo WORKER1
Processi:
- 2 x RD
