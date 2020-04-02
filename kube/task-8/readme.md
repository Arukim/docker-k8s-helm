# Task 8

## Create a cluster in Azure

* Open portal.azure.com, find Kubernetes services
* Create Kubernetes cluster
  * Select **SUBSCRIPTION**
  * Create a new resource group **RESOURCE_GROUP**
  * Enter cluster name **CLUSTER_NAME**
  * Selectvm  scale / vm count (if needed)
  * Review and Create
  * Create
* Cluster creation may take a while (15 mins)

## Setup local access

* Install az CLI 
  * https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest
* Login into az
  * az login
* az aks get-credentials --subscription **SUBSCRIPTION** --resource-group **RESOURCE_GROUP** --name **CLUSTER_NAME**
* Check cluster connectivity
  * $kubectl cluster-info

## Managing contexts

* View current context
  * $kubectl config current-context
* View all contexts
  * $kubectl config get-clusters
* View delete a cluster
  * $kubectl config delete-cluster clusterName
* Changes a context
  * $kubectl config use-context {{CONTEXT_NAME}}

## Playing around with cluster

* See nodes you have
  * $kubectl get nodes
* See pods
  * $kubectl get pods
* See namespaces
  * $kubectl get pods --all-namespaces -o wide
  * $kubectl get svc --all-namespaces -o wide

## Create a deployment

* Copy *.yaml from task-7
* Create config map from task 4
* Add resources request to container description
          resources:
              requests:
                cpu: 200m
* Apply local deployment
* Port-forward admin to localhost using port-forward command in a separate console
  * $kubectl port-forward service/web-admin {{LOCALPORT}}:{{REMOTEPORT}}
* Spam system with request in admin (1000, several times)
* Check hpa, pods


## View node dashboard

* az aks browse --resource-group myResourceGroup --name myAKSCluster

## Try cordon / drain commands

## Remove all

* Delete AKS / resource group
* Remove cluster locally