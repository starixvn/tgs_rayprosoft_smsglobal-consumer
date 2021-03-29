pipeline {
	agent any

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
					sh 'sudo docker build -f GlobalSmsConsumer/Dockerfile -t registry.gitlab.com/rayprosoft/turnkey-gaming-system/sms-global-consumer:dev . --secret id=nuget,src="/var/lib/jenkins/.nuget/NuGet/NuGet.Config"'
					sh 'sudo docker push registry.gitlab.com/rayprosoft/turnkey-gaming-system/sms-global-consumer:dev'
				}
			}
		}
	}
}