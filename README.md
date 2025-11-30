# SCM Designli API - Employee Management System

A RESTful API for employee management built with .NET 8, implementing CQRS pattern, FluentValidation, and MediatR.

## 📋 Requirements Fulfilled

This project fulfills all the technical requirements:

1. ✅ **Created with .NET 8**
2. ✅ **Employee class** with required fields:
   - Name
   - Last Name  
   - Birthday (Birthdate)
   - Email (Identity/Unique identifier)
   - Department (Additional field)
   - Timestamps (Additional fields)
3. ✅ **RESTful APIs with CRUD operations**:
   - Create (POST)
   - Read (GET - single and list)
   - Update (PUT)
   - Delete (DELETE - soft delete)
4. ✅ **In-Memory storage** - Data is not persisted to database
5. ✅ **Seed data** - 3 employee records loaded on startup:
   - Juan Pérez (IT Department)
   - María García (Human Resources)
   - Carlos Rodríguez (Finance)
6. ✅ **Swagger enabled** - Full API documentation available

## ✨ Additional Features

Beyond the basic requirements, this project includes:

- ✅ **CQRS Pattern** with MediatR for separation of concerns
- ✅ **FluentValidation** for robust input validation
- ✅ **Email Uniqueness Validation** - Prevents duplicate emails
- ✅ **Soft Delete** - Deleted employees are marked, not physically removed
- ✅ **Pagination & Search** capabilities
- ✅ **Clean Architecture** with separation of concerns
- ✅ **Docker Support** - Containerized application
- ✅ **Health Check** endpoint
- ✅ **Global Exception Handling** with standardized error responses
- ✅ **Best Practices**: Clean code, SOLID principles, comprehensive comments

## 🚀 Quick Start

### Running Locally

```bash
# Clone the repository
git clone <your-repo-url>
cd api-scm-designli

# Restore dependencies
dotnet restore

# Build
dotnet build

# Run
dotnet run --project ScmDesignli.Api/ScmDesignli.Api.csproj
```

Access the API at:
- **API**: http://localhost:5109
- **Swagger**: http://localhost:5109/swagger
- **Health**: http://localhost:5109/health

### Running with Docker

```bash
# Build image
docker build -t scm-designli-api:latest .

# Run container
docker run -d -p 5109:8080 --name scm-api scm-designli-api:latest

# Or use docker-compose
docker-compose up -d
```

## 📚 API Endpoints

| Method | Endpoint | Description | Notes |
|--------|----------|-------------|-------|
| `GET` | `/api/employees` | Get all employees | Active only |
| `GET` | `/api/employees/paginated` | Get paginated employees | Supports search |
| `GET` | `/api/employees/{id}` | Get employee by ID | 404 if not found |
| `POST` | `/api/employees` | Create new employee | Email must be unique |
| `PUT` | `/api/employees/{id}` | Update employee | Email must be unique |
| `DELETE` | `/api/employees/{id}` | Soft delete employee | Marks as deleted |
| `GET` | `/health` | Health check | Service status |

### Employee Model

```json
{
  "id": 1,
  "name": "John",
  "lastName": "Doe",
  "birthday": "1990-01-15T00:00:00Z",
  "email": "john.doe@designli.co",
  "department": 1,
  "isDeleted": false,
  "createdAt": "2024-01-01T00:00:00Z",
  "updatedAt": null,
  "deletedAt": null
}
```

### Departments (Enum)

The API includes detailed department descriptions in Swagger:

```json
[
  { "value": 1, "description": "Information Technology" },
  { "value": 2, "description": "Human Resources" },
  { "value": 3, "description": "Finance" },
  { "value": 4, "description": "Operations" },
  { "value": 5, "description": "Sales" },
  { "value": 6, "description": "Marketing" },
  { "value": 7, "description": "Management" },
  { "value": 8, "description": "Customer Service" }
]
```

### Seed Data

The application automatically loads 3 employees on startup:

1. **Juan Pérez**
   - Email: juan.perez@designli.co
   - Birthday: March 15, 1985
   - Department: IT

2. **María García**
   - Email: maria.garcia@designli.co
   - Birthday: July 22, 1990
   - Department: Human Resources

3. **Carlos Rodríguez**
   - Email: carlos.rodriguez@designli.co
   - Birthday: November 5, 1988
   - Department: Finance

### Validations

#### Create/Update Employee
- **Name**: Required, max 100 chars, letters and spaces only
- **LastName**: Required, max 100 chars, letters and spaces only
- **Birthday**: Required, must be in past, within last 100 years
- **Email**: Required, valid email format, max 200 chars, **must be unique**
- **Department**: Required, must be valid value (1-8)

#### Validation Error Response Example

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "Email": ["Email must be a valid email address"],
    "Birthday": ["Birthday must be in the past"],
    "Department": [
      "Department must be a valid department value. Valid values: 1 = IT, 2 = HumanResources, 3 = Finance, 4 = Operations, 5 = Sales, 6 = Marketing, 7 = Management, 8 = CustomerService"
    ]
  }
}
```

## 🐳 Docker

### Build

```bash
docker build -t scm-designli-api:latest .
```

### Run

```bash
docker run -d -p 5109:8080 scm-designli-api:latest
```

### Docker Compose

```bash
docker-compose up -d
docker-compose down
```

## 🌐 Deployment

### Azure Deployment

#### Option 1: Azure Container Apps (Recommended)

```bash
# Login
az login

# Create resource group
az group create --name scm-rg --location eastus

# Create container registry
az acr create --resource-group scm-rg --name scmacr --sku Basic

# Build and push
az acr build --registry scmacr --image scm-api:v1 .

# Create environment
az containerapp env create --name scm-env --resource-group scm-rg --location eastus

# Deploy
az containerapp create \
  --name scm-api \
  --resource-group scm-rg \
  --environment scm-env \
  --image scmacr.azurecr.io/scm-api:v1 \
  --target-port 8080 \
  --ingress external \
  --registry-server scmacr.azurecr.io
```

#### Option 2: Azure App Service

```bash
# Create app service plan
az appservice plan create \
  --name scm-plan \
  --resource-group scm-rg \
  --sku B1 \
  --is-linux

# Create web app
az webapp create \
  --resource-group scm-rg \
  --plan scm-plan \
  --name scm-api \
  --deployment-container-image-name scmacr.azurecr.io/scm-api:v1
```

#### Option 3: Azure Kubernetes Service (AKS)

```bash
# Create AKS cluster
az aks create \
  --resource-group scm-rg \
  --name scm-aks \
  --node-count 2 \
  --attach-acr scmacr

# Get credentials
az aks get-credentials --resource-group scm-rg --name scm-aks

# Deploy (create deployment.yaml first)
kubectl apply -f deployment.yaml
```

**deployment.yaml:**
```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: scm-api
spec:
  replicas: 3
  selector:
    matchLabels:
      app: scm-api
  template:
    metadata:
      labels:
        app: scm-api
    spec:
      containers:
      - name: api
        image: scmacr.azurecr.io/scm-api:v1
        ports:
        - containerPort: 8080
---
apiVersion: v1
kind: Service
metadata:
  name: scm-service
spec:
  type: LoadBalancer
  ports:
  - port: 80
    targetPort: 8080
  selector:
    app: scm-api
```

### AWS Deployment

#### Option 1: AWS ECS with Fargate (Recommended)

```bash
# Configure AWS CLI
aws configure

# Create ECR repository
aws ecr create-repository --repository-name scm-api --region us-east-1

# Login to ECR
aws ecr get-login-password --region us-east-1 | \
  docker login --username AWS --password-stdin <account-id>.dkr.ecr.us-east-1.amazonaws.com

# Tag and push
docker tag scm-api:latest <account-id>.dkr.ecr.us-east-1.amazonaws.com/scm-api:latest
docker push <account-id>.dkr.ecr.us-east-1.amazonaws.com/scm-api:latest

# Create ECS cluster
aws ecs create-cluster --cluster-name scm-cluster --region us-east-1

# Register task definition (create task-definition.json first)
aws ecs register-task-definition --cli-input-json file://task-definition.json

# Create service
aws ecs create-service \
  --cluster scm-cluster \
  --service-name scm-service \
  --task-definition scm-task \
  --desired-count 2 \
  --launch-type FARGATE \
  --network-configuration "awsvpcConfiguration={subnets=[subnet-xxx],securityGroups=[sg-xxx],assignPublicIp=ENABLED}"
```

**task-definition.json:**
```json
{
  "family": "scm-task",
  "networkMode": "awsvpc",
  "requiresCompatibilities": ["FARGATE"],
  "cpu": "256",
  "memory": "512",
  "containerDefinitions": [
    {
      "name": "scm-api",
      "image": "<account-id>.dkr.ecr.us-east-1.amazonaws.com/scm-api:latest",
      "portMappings": [{"containerPort": 8080, "protocol": "tcp"}],
      "environment": [{"name": "ASPNETCORE_ENVIRONMENT", "value": "Production"}]
    }
  ]
}
```

#### Option 2: AWS Elastic Beanstalk

```bash
# Install EB CLI
pip install awsebcli

# Initialize
eb init -p docker scm-api --region us-east-1

# Create Dockerrun.aws.json
cat > Dockerrun.aws.json << EOF
{
  "AWSEBDockerrunVersion": "1",
  "Image": {
    "Name": "<account-id>.dkr.ecr.us-east-1.amazonaws.com/scm-api:latest",
    "Update": "true"
  },
  "Ports": [{"ContainerPort": 8080, "HostPort": 80}]
}
EOF

# Create and deploy
eb create scm-env
eb deploy
eb open
```

#### Option 3: AWS App Runner

```bash
aws apprunner create-service \
  --service-name scm-api \
  --source-configuration '{
    "ImageRepository": {
      "ImageIdentifier": "<account-id>.dkr.ecr.us-east-1.amazonaws.com/scm-api:latest",
      "ImageRepositoryType": "ECR",
      "ImageConfiguration": {
        "Port": "8080",
        "RuntimeEnvironmentVariables": {"ASPNETCORE_ENVIRONMENT": "Production"}
      }
    }
  }' \
  --instance-configuration '{"Cpu": "1 vCPU", "Memory": "2 GB"}' \
  --region us-east-1
```

## 🏗️ Architecture

```
ScmDesignli/
├── ScmDesignli.Domain/          # Entities and Enums
├── ScmDesignli.Application/     # Commands, Queries, Validators
├── ScmDesignli.Infrastructure/  # Repositories, Data Access
└── ScmDesignli.Api/             # Controllers, Configuration
```

### Design Patterns
- **CQRS** (Command Query Responsibility Segregation)
- **Repository Pattern**
- **Mediator Pattern** (MediatR)
- **Dependency Injection**
- **Pipeline Behavior** for validation

## 🛠️ Technologies

- .NET 8
- ASP.NET Core
- MediatR
- FluentValidation
- Swagger/OpenAPI
- Docker

## 📝 Notes

- **In-Memory Storage**: Data is stored in `ConcurrentDictionary` and is lost on application restart (as required)
- **Soft Delete**: Deleted employees remain in memory but are marked with `IsDeleted = true` and hidden from queries
- **Email Validation**: Ensures no duplicate emails among active (non-deleted) employees
- **Seeded Data**: 3 employees (Juan Pérez, María García, Carlos Rodríguez) are automatically loaded on startup
- **Thread-Safe**: Repository uses `ConcurrentDictionary` and locking for thread-safe operations
- **Best Practices**: 
  - Clean Code with meaningful names and comprehensive XML comments
  - SOLID principles applied throughout
  - Separation of concerns (Domain, Application, Infrastructure, API layers)
  - Validation at multiple levels (FluentValidation + Domain rules)
  - Centralized exception handling with RFC 7807 problem details format

## 🎯 Project Structure

```
ScmDesignli/
├── ScmDesignli.Domain/              # Domain layer - Entities and Enums
│   ├── Entities/
│   │   └── Employee.cs              # Employee entity with all required fields
│   └── Enums/
│       └── Deparments.cs            # Department enum with descriptions
│
├── ScmDesignli.Application/         # Application layer - Business logic
│   ├── Commands/                    # CQRS Commands (Create, Update, Delete)
│   │   └── Employee/
│   ├── Queries/                     # CQRS Queries (Get, GetAll, Paginated)
│   │   └── Employee/
│   ├── Behaviors/                   # MediatR pipeline behaviors
│   │   └── ValidationBehavior.cs    # Automatic validation execution
│   └── Interfaces/
│       └── Repositories/            # Repository contracts
│
├── ScmDesignli.Infrastructure/      # Infrastructure layer - Data access
│   └── Persistence/
│       ├── Repositories/
│       │   ├── Repository.cs        # Generic in-memory repository
│       │   └── EmployeeRepository.cs # Employee-specific repository
│       ├── DataSeeder.cs            # Seeds the 3 required employees
│       └── Seeds/
│           └── EmployeeSeeding.cs   # Seed data definition
│
└── ScmDesignli.Api/                 # API layer - Controllers and config
    ├── Controllers/
    │   └── Employee/
    │       └── EmployeesController.cs # RESTful CRUD endpoints
    ├── Middleware/
    │   └── ExceptionHandlingMiddleware.cs # Global error handling
    ├── Filters/
    │   └── EnumSchemaFilter.cs      # Swagger enum documentation
    ├── Services/
    │   └── AppInitializer.cs        # Runs seeder on startup
    └── Program.cs                   # App configuration + Swagger setup
```


 
