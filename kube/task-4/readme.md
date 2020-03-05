# Task 4

## Config Maps, Secrets

* Create config map for application
    * $kubectl create configmap app --from-file=config-maps/
    * kubectl create configmap app --from-file=configMaps/ -o yaml --dry-run | kubectl replace -f -
* View config 
  * $kubectl get configmap app -o yaml
* Delete a config map
  * $kubectl delete configmap app
* Create a secret
  * $kubectl apply -f .\secret.yaml
* View a secret
  * $kubectl get secret
  * $kubectl get secret db-connection -o yaml
* Delete a secret
  * $kubectl delete -f ./secret.yaml