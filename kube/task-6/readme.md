# Task 6

## Create service and deployment

* View both YAML and insert correct machine names (use equal to your namespace)
* Create service, deployment and ingress
  * NB service and deployment in a single file
  * NB we need an ingress
  * $kubectl apply -f ./
* view service description
  * $kubectl describe service web-admin
* View page at http://hel.westeurope.cloudapp.azure.com/{NAMESPACE}/admin
* delete deployment and service
  * $kubectl delete -f ./
