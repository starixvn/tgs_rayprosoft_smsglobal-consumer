pipeline {
	agent any
	
	environment {
		TAG = sh(script: "echo dev-`date +%Y%m%d%H%M`", returnStdout: true).trim()
		Registry = "registry.gitlab.com/rayprosoft/turnkey-gaming-system/"
		gitlab_user_pwd = credentials('gitlab-user-pwd')
		Component = "sms-global-consumer"
	}
	
	stages {
		stage('Clone') {
			steps {
				git branch: "main", url: 'https://gitlab.com/rayprosoft/smsglobal-consumer.git' , credentialsId: 'gitlab-user-pwd'
			}
		}
	
		stage('Build') {
			steps {
				sh "dotnet build GlobalSmsConsumer.sln"
			}
		}
		
		stage('Docker Image') {
			steps {
				script {
					sh 'sudo docker build -f GlobalSmsConsumer/Dockerfile -t registry.gitlab.com/rayprosoft/turnkey-gaming-system/${Component}:${TAG} . --secret id=nuget,src="/var/lib/jenkins/.nuget/NuGet/NuGet.Config"'
					sh 'sudo docker push registry.gitlab.com/rayprosoft/turnkey-gaming-system/${Component}:${TAG}'
					
					withKubeConfig([credentialsId: 'k8s-kube-config', serverUrl: 'https://192.168.50.105:6443']) {
						sh "kubectl set image deployment/${Component} ${Component}=${Registry}${Component}:${TAG} --record -n dev"
					}
				}
			}
		}
		
		stage('Deployment') {
			steps {
				script {
					withKubeConfig([credentialsId: 'k8s-kube-config', serverUrl: 'https://192.168.50.105:6443']) {
						sh "kubectl set image deployment/${Component} ${Component}=${Registry}${Component}:${TAG} --record -n dev"
					}
				}
			}
		}
	}
}