# 📱 Mobile Técnico - Android Java

Aplicação móvel para técnicos receberem e resolverem chamados.

## 🚀 Execução

1. Abrir no Android Studio
2. Sync Gradle
3. Run 'app' (Shift+F10)

## 👤 Usuário de Teste

- Email: `tecnico@helpdesk.com`
- Senha: `Tecnico@123`

## 📝 Configuração

### API URL

Edite `ApiClient.java`:

```java
// Emulador Android
private static final String BASE_URL = "http://10.0.2.2:5000/api/";

// Dispositivo físico (use seu IP local)
// private static final String BASE_URL = "http://192.168.1.100:5000/api/";
```

### Firebase (Opcional)

1. Firebase Console: https://console.firebase.google.com
2. Criar projeto
3. Adicionar app Android
4. Package: `com.helpdesk.tecnico`
5. Download `google-services.json`
6. Colocar em `app/google-services.json`

## 📁 Estrutura

- **activities/**: LoginActivity, MainActivity
- **network/**: ApiClient, AuthInterceptor, ApiService
- **models/**: Chamado, LoginRequest, LoginResponse
- **database/**: Room Database (cache offline)
- **services/**: MyFirebaseMessagingService (push)

## ✨ Funcionalidades

✅ Login JWT  
✅ Listar chamados atribuídos  
✅ Visualizar detalhes e IA  
✅ Push notifications (FCM)  
✅ Cache offline (Room)  
✅ Material Design  

## 🔧 Dependências

- Retrofit2 2.9.0
- Room 2.6.1
- Firebase Messaging 23.4.0
- Material Components 1.11.0
