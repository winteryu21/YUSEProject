# 로그라이크 프로젝트

## 📂 프로젝트 구조

```text
📦 Project_Root
 ┃
 ┣━━ 📂 docs/                      # 📝 기획서, 회의록 및 모든 문서 산출물
 ┃
 ┗━━ 🎮 roguelike/                 # 🏗️ Unity 프로젝트 루트
      ┃
      ┗━━ 📦 Assets/               # 💎 게임의 모든 리소스 및 스크립트
           ┃
           ┣━━ 🎬 00_Scenes/       # 씬 파일 (.unity)
           ┃
           ┣━━ 📜 01_Scripts/      # C# 소스 코드
           ┃    ┣━━ ⚙️ _Core/      # 게임 매니저 (GameManager, Audio, Save 등)
           ┃    ┣━━ 👾 Enemies/    # 몬스터 AI 및 스폰 로직
           ┃    ┣━━ ⚔️ Gameplay/   # 핵심 메커니즘 (경험치 구슬, 보상 등)
           ┃    ┣━━ 🎒 Loot/       # 장비, 아이템 데이터 및 로직
           ┃    ┣━━ 👤 Player/     # 플레이어 제어 및 스탯
           ┃    ┣━━ 🧪 Test/       # 테스트 및 디버그용
           ┃    ┗━━ 🖥️ UI/         # HUD, 메뉴, 패널 제어
           ┃
           ┣━━ 🧱 02_Prefabs/      # 프리팹 (재사용 가능한 게임 오브젝트)
           ┃    ┣━━ 💎 Acquisition/# 획득 가능 오브젝트 (경험치 구슬 등)
           ┃    ┣━━ 👾 Enemies/    # 적 캐릭터 프리팹
           ┃    ┣━━ 🌳 Environment/# 맵, 벽, 배경 요소
           ┃    ┣━━ ✨ FX/         # 이펙트, 파티클
           ┃    ┣━━ 🎁 Loot/       # 장비 및 아이템 프리팹 (GameObject)
           ┃    ┣━━ 👤 Player/     # 플레이어 프리팹
           ┃    ┗━━ ⚙️ System/     # 시스템 프리팹 (매니저, UI 캔버스 등)
           ┃
           ┣━━ 🎨 03_Arts/         # 리소스 에셋
           ┃    ┣━━ 🎵 Audio/      # BGM, SFX
           ┃    ┣━━ 🔤 Fonts/      # 폰트 파일
           ┃    ┗━━ 🖼️ Sprites/    # 2D 이미지, 아이콘, 도트
           ┃
           ┣━━ 🎮 InputSystem_Actions  # 입력 키 매핑 설정
           ┣━━ 🔧 Settings/            # URP 및 렌더링 설정
           ┣━━ 📦 TextMesh Pro/        # (Unity System) 텍스트 렌더링 에셋
           ┗━━ 📦 UI Toolkit/          # (Unity System) UI 툴킷 리소스
```

---

## 🔀 Git Branch 전략
- **main**: Release branch 
    - **직접 수정 및 PR 절대 금지!!!**
- **develop**: Develop test branch
    - 개발된 기능을 병합하고 테스트하는 브랜치입니다.
    - 문서 작업 또한 이 브랜치로 병합됩니다.
- **feat/#{issue_number}/{feature_name}**: Normal working branch
    - 기능 개발 브랜치입니다. 일반적인 프로젝트 작업은 모두 이 브랜치에서 이루어집니다.
- **hotfix/#{issue_number}/{hotfix_name}**: Bug working branch
    - main 브랜치에 병합한 기능이 오류를 일으키는 경우, 빠르게 수정하는 브랜치입니다.
- **doc/#{issue_number}/{doc_name}**: Document branch
    - 문서 작업용 브랜치입니다.
- **refactor/#{issue_number}/{name}**: Refactoring branch
    - 리팩토링 전용 브랜치입니다. 코드 리팩토링, 프로젝트 구조 변경 등이 포함됩니다.

### 예시:

- 브랜치 이름 예시: `feat/18/playermove`
- **한 브랜치는 한 작업만!**
- 커밋 메시지 예시: `doc: Use Case 7~13 내용 추가`, `feat: 플레이어 이동 구현`
- Pull Request 제목은 **커밋 메시지와 동일하게**
- Pull Request 본문에는 issues number 포함할 것(#16 이런 식으로 샵 기호까지)

---

## 작업 간단 가이드

1. 자신의 fork 저장소와 원본 저장소 간 develop 브랜치의 Sync가 맞는지 확인
2. 작업 유형에 따른 브랜치 생성(Git Branch 전략 항목 참조)
    - 원본 저장소 Issues 탭에서 {issue_number} 확인
    - upstream(원본 저장소)에서 브랜치 생성하는 걸 추천
3. 원본 저장소 develop 브랜치로 Pull Request
