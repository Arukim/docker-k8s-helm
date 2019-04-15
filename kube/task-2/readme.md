# Task 2

## kubectl basic manipulations
 
* Browse contents of hello-pod.yaml
* Create a pod using imperative syntax
  * $kubectl create -f ./hello-pod.yaml
  * $kubectl create -f ./hello-pod.yaml --dry-run -o yaml
* Check information about pod (s)
  * $kubectl get pod
  * $kubectl get pod -o wide
  * $kubectl logs hello-world --timestamps=true
  * $kubectl describe pod hello-world
* Try to create one more pod
  * $kubectl create -f hello-pod.yaml
  * $kubectl apply -f hello-pod.yaml
* Delete a pod and monitor deletion process
  * $kubectl delete -f hello-pod.yaml
  * $kubectl get pod
  * $kubectl describe pod hello-world
* Create a pod using declarative syntax
  * $kubectl apply -f hello-pod.yaml
  * $kubectl describe pod hello-pod.yaml
* Add one more label in pod definition (language = bash)
  * $kubectl apply -f hello-pod.yaml
* Finish task with cleaning up resources
  * $kubectl delete -f hello-pod.yaml
  

## Commands list

* Imperative resource managment
  * Create a resource using it's configuration
  
$kubectl create -f 'path-to-file'

$kubectl create -f 'path-to-directory'
  * Create a resource and allow declarative management
  
$kubectl create -f 'file|dir' --save-config
  * Update existing resource with imperative command
  
$kubectl replace -f 'file|dir'
  * Delete an existing resource

$kubectl delete -f 'file|dir'

* Declarative resource managment
  
$kubectl apply -f 'file|dir'

* View resources

$kubectl get pods

* View logs

 $kubectl logs {resource} --timestamps=true

* View nodes

 $Kubectl get nodes