# BoardMVC

ASP.NET Core MVC + Entity Framework Core로 만든 게시판 CRUD 예제

## 사용 기술

- ASP.NET Core MVC
- Entity Framework Core (SQLite)

## 로컬 실행 방법

### 1. 필요 패키지 설치

Visual Studio에서 도구 > NuGet 패키지 관리자 > 패키지 관리자 콘솔을 열고,
상단 "기본 프로젝트"를 BoardMVC로 선택한 뒤 아래 명령어 실행:

    Install-Package Microsoft.EntityFrameworkCore.Sqlite
    Install-Package Microsoft.EntityFrameworkCore.Tools
    Install-Package Microsoft.EntityFrameworkCore.Design

### 2. DB 마이그레이션

같은 콘솔에서 아래 명령어를 순서대로 실행:

    Add-Migration InitialCreate
    Update-Database

실행하면 프로젝트 폴더에 board.db 파일이 생성됨

### 3. 실행

F5로 실행 후 주소창에 /Posts 입력하여 접속

    https://localhost:{포트번호}/Posts

## 폴더 구조

    Controllers/PostsController.cs   - CRUD 로직
    Models/Post.cs                    - 게시글 모델
    Data/AppDbContext.cs              - DB 컨텍스트
    Views/Posts/                      - 게시판 화면들
