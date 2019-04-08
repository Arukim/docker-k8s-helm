# Docker Task #1

Play with docker and linux shell

## Task

- Check that local docker installation works
  - $docker version
  - $docker info
  - $docker run hello-world
- Pull busybox image
  - $docker pull busybox
- View local images
  - $docker images
- Run the busybox
  - $docker run busybox
- Check containers
  - $docker ps
- Run a command in busybox
  - $docker run busybox echo "hello world"
- Check containers (including closed)
  - $docker ps -a 
- Enter docker in interactive mode
  - $docker run -i busybox sh
    - $ls
    - $top
    - press q to exit
    - $ls bin
    - $rm -rf bin
    - $ls bin
    - $exit
- $docker run -i busybox sh
  -  $ls bin
  -  exit  
