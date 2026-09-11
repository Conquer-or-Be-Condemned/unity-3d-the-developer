# Team Workflow — 팀 협업 규칙

기준일: 2026-09-10

> 이 문서는 팀원별 역할과 협업 규칙을 정의한다. 전체 개발 순서와 단계별 통과 기준은 [DEVELOPMENT_FLOW.md](DEVELOPMENT_FLOW.md)를 따른다.

## 1. PL 역할

소규모 팀의 PL은 프로젝트 방향을 정하면서 핵심 기능도 직접 구현한다. 이런 형태를 `Player-Coach`라고 부를 수 있다.

- 프로젝트 목표와 현재 Milestone을 한 문장으로 설명한다.
- 우선순위를 정하고 동시에 진행하는 기능 수를 제한한다.
- Task의 완료 조건을 구현 전에 합의한다.
- 팀원이 막힌 문제를 해결하고 필요한 결정을 미루지 않는다.
- 매주 실행 가능한 Build를 직접 확인한다.
- 기획 변경이 일정과 기존 작업에 미치는 영향을 기록한다.
- Milestone 통과 기준을 확인하고, 통과 전 다음 Content 대량 제작을 막는다.
- 자신의 구현 코드도 다른 팀원의 Code Review를 받는다.

PL이 피해야 할 일:

- 모든 세부 구현 방법을 혼자 결정하기
- 회의 중 나온 아이디어를 즉시 확정 기능으로 취급하기
- 마감일과 완료 조건이 없는 Task를 여러 개 동시에 시작하기
- Core Fun이 검증되지 않은 System의 Final Art부터 완성하기
- 담당자에게 구체적인 완료 조건 없이 “알아서 만들어 달라”고 전달하기

## 2. 팀원별 역할과 기능 담당

한 사람이 여러 역할을 맡을 수 있지만 기능마다 최종 담당자는 한 명만 둔다. 담당자는 구현을 전부 혼자 한다는 뜻이 아니라, 해당 기능의 진행과 품질을 끝까지 책임진다는 뜻이다.

### 공통 원칙

실명 대신 `A~E`를 사용하며 A는 PL이다. 역할은 고정 직무가 아니라 플레이어에게 전달되는
**제품 영역(Product Area)** 의 Owner다. 각 Task의 주 담당자는 한 명만 두고, A도 기획만 하지 않고
Core와 Meta UI를 직접 구현한다. Build·CI·QA·Release는 E 또는 특정 개인의 독점 책임이 아니라,
각 Feature Owner의 Done 조건이며 통합 Build 확인은 Sprint마다 순환한다.

역할 범위와 AI/온라인의 세부 경계는 [TEAM_ROLE_OWNERSHIP.md](TEAM_ROLE_OWNERSHIP.md)가 유일한
Source of Truth다. 아래 표는 일상적인 Task 배정에 사용할 요약이다.

| 담당 | 제품 영역 | Planet / 전투 | Spaceship / 허브 | Cross 책임 |
|---|---|---|---|---|
| A | Project Lead / Core Systems & UI Architect | Match lifecycle, mode rule, 결과/보상 | Mission·Research·Economy UI 흐름 | Core, 저장/경제, 세션/권한/스냅샷, platform 기반, UI architecture |
| B | Player, Cooperative UX & Cinematic Director | Player combat, Combat HUD, 튜토리얼, 실시간 연출 | Console 조작 UX, 전환 연출 | 입력/카메라, Invite·SOS·Ping·Chat UX, 컷씬 디렉팅 |
| C | World, Mission & Agent Experience Designer | Planet/mission/environment, Ally Player Agent | Hub 공간과 다이아제틱 UI 배치 | 월드 콘텐츠 data, Rival Player Agent **(Deferred)** |
| D | Defense, Progression & Balance Designer | Turret/Power/Defense | 연구·해금·강화 | 전투·경제·성장·AI 난이도 data/balance |
| E | Enemy, Encounter & AI Platform Engineer | Enemy/Boss AI, Horde Director | AI 공통 실행 기반 | behavior runtime, 감지·경로·타게팅·action executor |

### Planet과 Spaceship 연결 담당

| 연결 지점 | 주 담당 | 지원 | 완료 판단 |
|---|---|---|---|
| Spaceship → Planet Launch / Return | A | B, C | Mission/Result/Profile 상태가 정확히 왕복 |
| Upgrade → Turret/Power 적용 | D | A, B | 연구 결과가 다음 MatchConfig와 전투에 적용 |
| Planet Expansion → Mission 정보 갱신 | C | A, B | 구역 변화가 월드와 Mission UI에서 일치 |
| 유저 이탈 → Ally AI 대체/복귀 | C | A, B, E | 권한·표현·Agent 행동이 끊기지 않음 |
| 통합 Build와 Playtest | Sprint 순환 | 전체 팀 | 왕복 Flow를 중단 없이 완주 |

### 권장 Code Review 조합

| 작성자 | 기본 검토자 |
|---|---|
| A | D 또는 E |
| B | A 또는 D |
| C | A 또는 E |
| D | A 또는 E |
| E | A 또는 D |

GitHub Approval은 Merge 필수 조건으로 두지 않는다. 다만 공용 System, Scene 구조, Save, Build 설정처럼 영향 범위가 큰 PR은 아래 조합으로 검토를 요청한다. 작은 독립 변경은 작성자가 직접 확인하고 CI가 통과하면 Merge할 수 있다.

## 3. 작업 관리

### Backlog 구조

```text
Epic(큰 목표): Planet 1 Vertical Slice
└─ Feature(기능): 전력 기반 Turret 선택
   └─ Task(작업): Laser Turret을 공통 선택 UI에 연결
      └─ Bug(오류): Laser 비활성화 후 Power가 중복 반환됨
```

모든 Task에는 다음이 있어야 한다.

- Player에게 주는 가치 또는 해결할 문제
- 주 담당자 한 명
- 예상 작업 시간
- 선행 작업
- 구체적인 완료 조건
- 확인 방법

완료 조건이 없는 Task는 Sprint에 넣지 않는다.

### 우선순위

- P0: Build나 진행을 막는 문제
- P1: 현재 Milestone의 핵심 Gameplay
- P2: 품질과 사용성 개선
- P3: 나중에 검토할 아이디어

새 아이디어는 기본적으로 P3 Product Backlog에 기록한다. 현재 Milestone의 다른 Task를 빼거나 일정을 조정하기로 PL이 결정하지 않는 한 즉시 착수하지 않는다.

## 4. 팀 운영 주기

소규모 파트타임 팀은 2주 단위 Sprint와 주 1회 Build 검토를 기본으로 한다. 단, 프로젝트 안정화와 기획 확정을 위한 Sprint 0만 1주로 운영한다.

### Sprint 계획 — 30분

- 지난 Sprint 또는 최신 Build 직접 실행
- 완료 / 미완료 Task 확인
- 현재 Milestone의 가장 큰 위험 요소 한 가지 선정
- 이번 Sprint 목표를 한 문장으로 합의
- 각자 동시에 진행할 핵심 Task 최대 1~2개 배정
- 개인 가용 시간의 약 70%만 계획에 사용

### 비동기 진행 공유 — 작업일마다

각자 다음 세 줄만 공유한다.

```text
Done: 무엇을 끝냈는가
Next: 무엇을 할 것인가
Blocker: 누구의 결정이나 작업이 필요한가
```

### 통합 및 Playtest — 주 1회

- 각자 Editor 화면을 보여주는 대신 같은 통합 Build를 Play한다.
- 최소 한 명은 자신이 만들지 않은 기능을 Test한다.
- 발견된 문제는 재현 순서와 기대 결과를 기록한다.
- 다음 Build에서 반드시 고칠 세 가지를 정한다.

## 5. Git Workflow

### Branch 규칙

- `main`: 언제나 실행 및 Build 가능한 상태
- `feature/<issue>-<name>`: 기능 구현
- `fix/<issue>-<name>`: Bug 수정
- `content/<issue>-<name>`: Map, Prefab, Data Content
- `chore/<issue>-<name>`: 설정, 문서, 자동화

공유된 `main`에는 일반적으로 force push하지 않는다. 잘못 올라간 Commit은 Revert Commit으로 수정한다.

### Pull Request (PR)

- 하나의 PR은 하나의 목적만 가진다.
- 변경 이유, 확인 방법, 영향받는 Scene을 적는다.
- UI나 Gameplay 변경은 Screenshot 또는 짧은 Video 첨부를 권장한다.
- 공용 System, Scene 구조, Save, Build 설정 변경은 관련 담당자에게 검토를 요청한다.
- 작은 독립 변경은 다른 팀원의 Approval 없이 작성자 확인과 CI 통과 후 Merge할 수 있다.
- CI가 실패하면 Merge하지 않는다.
- 큰 Scene 충돌이 예상되면 작업 전에 Scene 또는 Prefab 담당 범위를 나눈다.

### Unity Asset 작업 규칙

- `Version Control Mode`: Visible Meta Files
- `Asset Serialization Mode`: Force Text
- Asset과 `.meta`를 항상 함께 Commit한다.
- Asset 이동과 Rename은 가능한 Unity Editor 안에서 한다.
- `Library`, `Temp`, `Logs`, `obj`, 사용자 IDE 폴더는 Commit하지 않는다.
- 같은 `.unity` Scene을 두 명이 동시에 수정하지 않는다.
- 반복 배치물은 Prefab으로 분리한다.
- Expansion Sector는 가능하면 Sector Prefab 또는 Additive Scene으로 나눠 충돌 범위를 줄인다.
- Binary Large Asset은 필요 시 Git LFS 적용을 검토한다.

## 6. 착수 조건 (Definition of Ready)

다음 조건을 만족해야 구현을 시작할 수 있다.

- 해결할 문제를 한 문장으로 설명 가능
- 포함 범위와 제외 범위가 있음
- 완료 조건을 실제로 확인할 수 있음
- 필요한 기획 또는 참고 자료가 준비됨
- 선행 작업과 담당자가 확인됨
- 수정할 Scene, Prefab 또는 Code Module이 알려져 있음

## 7. 완료 조건 (Definition of Done)

기능은 Code를 작성했을 때가 아니라 다음을 모두 만족할 때 Done이다.

- Task의 완료 조건 충족
- Unity Compile Error 0
- Play 중 새로운 Console Error 0
- Missing Script와 Broken Prefab Reference 0
- Asset과 `.meta`가 함께 Commit됨
- 관련 Scene에서 직접 Test함
- 기존 Core Flow가 망가지지 않았는지 재검사 완료
- Code Review와 CI 통과
- 필요한 Docs 또는 Data 갱신

## 8. Bug 기록 양식

```text
제목: [기능/Scene] 실제 문제
Build/Commit:
실행 환경:
재현 순서:
1.
2.
3.
기대 결과:
실제 결과:
발생 빈도:
Screenshot/Video/Log:
심각도: Blocker / Major / Minor
```

### 심각도

- Blocker: Build 불가, Crash, Save 손상, 진행 불가
- Major: 핵심 기능 오작동, 우회 방법은 있음
- Minor: Visual, Audio, 문구 등 영향이 낮은 문제

## 9. 변경 관리

Core Rule 또는 제작 범위를 바꾸려면 다음을 기록한다.

```text
변경 제안:
해결할 Player 문제:
기대 효과:
추가되는 작업:
제거하거나 미룰 작업:
일정 영향:
시험 및 검증 방법:
결정 담당 / 날짜:
```

기능을 추가하면서 일정과 기존 범위를 모두 그대로 유지하는 결정은 허용하지 않는다. 추가하려면 같은 크기의 Task를 제거하거나 Milestone을 조정한다.

## 10. PL 주간 체크리스트

- 이번 Sprint 목표가 한 문장인가?
- 팀원마다 진행 중인 Task가 1~2개 이하인가?
- `main`이 실행 가능한가?
- 최신 통합 Build를 직접 Play했는가?
- Core Fun과 관계없는 Task가 끼어들지 않았는가?
- 막힌 결정이 이틀 이상 방치되지 않았는가?
- 새 아이디어를 현재 Sprint와 분리했는가?
- 일정이 아니라 통과 기준으로 Milestone을 판단했는가?
- 다음 주 가장 큰 위험 요소가 무엇인지 알고 있는가?
