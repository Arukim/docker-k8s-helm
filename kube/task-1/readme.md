# Task 1

Configure connection to AKS

## Get configuration from Azure

* Login to azure-cli
  * $az login
  * Enter EPAM credentials
* Get cluster credentials
  * $az aks get-credentials --resource-group k8s-group-one --name hel --subscription 82a0fb32-64b4-4985-a1ba-2c0400ab5d16
* Switch to your personal workspace
  *  kubectl config set-context hel --namespace {(11-64)}
* Logout from azure-cli
  * $az logout
* Check cluster availability
  * $kubectl cluster-info


## Commands list

* Check current configuration
  $kubectl config view 
* View current cluster info
  $kubectl cluster-info
* Add new cluster to configuration
  $kubectl set-cluster {name} --server={clusterURI}
* Add new credentials
  $kubectl config set-credentials {username} --token {token}
* Add context **S-NN** Where N is your Mac 
  $kubectl config set-context {contextName} --cluster={clusterName} --namespace={your_namespace} --user={username}



