# Team Role Ownership — 최종 5인 제품 영역 모델

상태: 합의 기준 / `TEAM_WORKFLOW.md`의 역할표를 대체하는 Source of Truth
기준일: 2026-09-11

## 1. 목적과 운영 원칙

이 문서는 Planet, Spaceship, Core를 분리하면서도 기능이 서로 끊기지 않도록 5개의 **제품 영역(Product Area)** 을 정의한다.
CI/CD, QA, Build, Release를 별도 직책으로 만들지 않는다. 각 Feature Owner가 자신의 기능을 테스트·문서화·통합 가능한 상태로 만드는 것이 Done의 일부다.

- 기능 묶음마다 최종 Owner는 한 명이다. Owner는 혼자 구현한다는 뜻이 아니라, 범위·품질·통합 상태를 끝까지 책임진다는 뜻이다.
- A는 Core의 단일 Owner다. 단, 모든 콘텐츠를 직접 구현하는 병목이 되어서는 안 된다.
- 인간 플레이어와 AI는 동일한 Player Action/Command 계약을 통과한다.
- AI, 밸런스, 월드 콘텐츠는 코드에 수치를 고정하지 않고 Definition/Profile 데이터로 조정한다.
- PvP와 `RivalPlayerAgent`는 **Deferred**다. 협동 MVP를 막는 선행 작업이 아니다.

## 2. 최종 역할표

| 담당 | 직책 | Planet / 전투 | Spaceship / 허브 | 횡단 책임 |
| --- | --- | --- | --- | --- |
| A | **Project Lead / Core Systems & UI Architect** | Match lifecycle, mode rules, 결과·보상 | Mission·Research·Economy UI 흐름 | Core 계약, 세션/권한/스냅샷, Profile·저장·경제 정책, Platform/Network 기반, UI Architecture |
| B | **Player, Cooperative UX & Cinematic Director** | 플레이어 전투·상호작용·전투 HUD·튜토리얼·실시간 연출 | Console 조작 UX, 우주선/미션 전환 연출 | 입력·카메라·접근성, 초대/매칭/SOS/핑/채팅 UX, 컷씬 디렉팅 |
| C | **World, Mission & Agent Experience Designer** | 행성 구조·구역 확장·기믹·미션/이벤트, 아군 AI 플레이어 | 우주선 공간·다이아제틱 UI 배치·월드 연출 기반 | `AllyPlayerAgent` 정책/구현, `RivalPlayerAgent` 정책/구현 **(Deferred)**, 월드 콘텐츠 데이터 |
| D | **Defense, Progression & Balance Designer** | 터렛·제어장치·전력·타게팅·방어선 전술 | 연구소·해금·강화·자원 소비 경험 | 전투·경제·성장 밸런스, 난이도/AI 프로필, 데이터 검증 |
| E | **Enemy, Encounter & AI Platform Engineer** | 적 개체·엘리트·보스 AI, 웨이브·스폰·호드 디렉터 | 적/AI 기능이 요구하는 공통 실행 기반 | AI framework, 감지·경로·타게팅·행동 실행 공통 기능 |

## 3. 역할별 책임과 명시적 경계

### A — Project Lead / Core Systems & UI Architect

**Owner**

- `AppBootstrap`, `AppRoot`, `AppServices`, Composition Root와 App lifecycle
- `MatchSession`, `MatchState`, `GameSimulation`, `ModeRules`, Match lifecycle
- 공용 ID, `Command`, `Event`, `Result`, `Snapshot`, 오류 모델과 Content schema/versioning
- `PlayerProfile`, `RewardReceipt`, 재화·해금·연구의 business rule과 local binary cache contract
- Steam identity/backend 검증 adapter의 경계, Authority model, reconnect/host migration state contract
- UI navigation, Presenter/View contract, 공통 modal/loading/input-blocking, Mission/Research/Result/Economy UI
- 레거시 이식 기준, Core 테스트와 Architecture Decision Record

**직접 Owner가 아닌 것**

- 플레이어 조작감·전투 HUD의 세부 표현(B)
- Planet/Hub 공간과 Prefab/Scene authored content(C)
- 터렛 실제 기능 및 수치(D)
- 적 개체 행동/Director/AI 실행 기반(E)

> A는 모든 기능을 직접 만드는 사람이 아니라, 모든 Feature가 같은 규칙으로 연결되게 하는 Core와 Meta UI의 Owner다.

### B — Player, Cooperative UX & Cinematic Director

**Owner**

- 플레이어 이동·조준·사격·능력·피격·회복·상호작용의 Unity presentation
- Combat HUD, 입력 피드백, 카메라, 타게팅, 접근성, 튜토리얼/Onboarding
- Lobby/Invite/Random Match/SOS/Ping/Chat/Reconnect의 **사용자 경험과 화면 흐름**
- 컷씬의 의도·카메라·Unity Timeline·UI/입력 전환·스킵 정책 및 최종 연출 품질
- Hub Console의 player-facing interaction pattern
- 인간/AI가 공통으로 사용할 Player Action 계약의 presentation 측

**경계**

- 컷씬의 공간/환경/오브젝트는 C, 보스/웨이브 행동은 E, 해금·전투적 의미는 D, 상태 전이는 A가 제공한다.
- B는 웨이브·보상·재화의 원본 상태를 직접 변경하지 않는다.
- 컷씬은 프리렌더 영상이 아닌 **짧고 스킵 가능한 실시간 인게임 연출**을 기본 범위로 한다.

### C — World, Mission & Agent Experience Designer

**Owner**

- Planet topology, Sector/Expansion, 방어 거점, 동선, Collision, Spawn 접근로, Boss arena
- 날씨·시간대·중력·시야·위험 지형, 상호작용 오브젝트, 행성별 환경 기믹
- 생존/방어 외 미션 목표, Dynamic Event, 구역 해금, Challenge/Resource Run 등 월드 콘텐츠
- Hub interior, 연구소/조종석/통신 구역의 공간 배치, 다이아제틱 UI의 월드 배치·시각적 안내
- `PlanetDefinition`, `MissionDefinition`, `SectorDefinition`, `EnvironmentModifier` 등 authored content
- `AllyPlayerAgent`: 협동 이탈자 대체, 지원/생존/목표 우선순위, 행성·미션별 행동 프로필과 구체 구현
- `RivalPlayerAgent` **(Deferred)**: 1:1 상대 봇의 공간 활용, 목표·전략·압박 정책과 구체 구현

**경계**

- C의 Player Agent는 E의 공통 AI framework와 B의 Player Action을 재사용한다. 탐색·감지·행동 실행 기반을 재구현하지 않는다.
- C는 적 개체 AI 또는 호드 웨이브 로직을 Owner로 갖지 않는다.
- C는 전투 수치의 최종 Owner가 아니며 D의 Profile을 사용한다.

### D — Defense, Progression & Balance Designer

**Owner**

- Cannon/Missile/Laser 등 터렛, 제어장치, 전력 소비/회수, 설치, Targeting, 방어선 전술
- Engineering/Research feature, 무기·터렛 해금·강화와 resource 소비 경험
- `TurretDefinition`, `PowerDefinition`, upgrade/counter table, reward/economy tuning data
- 전투·경제·성장·웨이브 난이도·AI profile의 수치 설계, simulation/playtest 기준과 결과 반영

**경계**

- 공용 State/Command schema와 보상 transaction rule은 A와 합의해 사용한다.
- 적 행동을 구현하지 않고, 적/AI가 사용할 난이도와 상성 데이터를 제공한다.

### E — Enemy, Encounter & AI Platform Engineer

**Owner**

- 기본/탱커/원거리/암살자/엘리트/보스의 적 개체 행동, 타겟팅, 스킬, Boss phase
- `EncounterDirector`: 웨이브 구성, spawn 위치/타이밍, 적 조합, 압박 곡선, 보스 투입
- AI framework, behavior/state runtime, 감지, 경로, 타게팅, 공통 action executor
- `EnemyArchetype`, `BossDefinition`의 행동 측 구현과 데이터 연결

**경계**

- `AllyPlayerAgent`와 `RivalPlayerAgent`의 목표/전략/행동 정책은 C가 Owner다. E는 이들이 사용할 기반을 제공하고 코드 리뷰한다.
- Network authority/session의 계약과 Steam/backend adapter 경계는 A가 Owner다. E의 AI가 Network SDK나 MatchState를 직접 변경하지 않는다.

## 4. AI 분해와 협업 계약

| AI 종류 | 최종 Owner | 협업 | 설명 |
| --- | --- | --- | --- |
| Enemy Actor AI | E | D: 상성/수치, C: 공간 | 일반 적·엘리트·보스의 전투 행동 |
| Encounter / Horde Director | E | A: ModeRules, D: 난이도 profile, C: spawn layout | 웨이브·스폰·압박을 통솔 |
| Ally Player Agent | C | E: framework, B: Player Action, D: profile, A: authority | 이탈 유저의 AI 대체와 협동 지원 |
| Rival Player Agent **(Deferred)** | C | E: framework, B: Player Action, D: economy/profile, A: PvP ModeRules | 1:1 봇의 전략·업그레이드·공간 활용 |

AI는 모두 `GameCommand`를 생성하고 A의 Authority/Simulation이 검증·처리한다. EventBus는 로컬 전달 수단일 뿐 네트워크 동기화 수단이 아니다.

## 5. 온라인·호스트 이탈의 소유 경계

| 관심사 | Owner | 협업 |
| --- | --- | --- |
| Steam identity, backend validation, lobby/transport adapter 경계 | A | B: UX, E: AI/networked action 검증 |
| Authority, Snapshot, reconnect, host migration state rule | A | B/C/E/D |
| Invite, random match, SOS, ping, chat, reconnect 화면/입력 | B | A |
| 이탈 유저 → 아군 AI 대체와 복귀 정책 | C | A: authority, E: runtime, B: UX |
| 협동 전투 수치와 보상 | D | A/C/E |

Host migration은 AI fallback과 별개다. 협동 MVP가 안정된 뒤 Snapshot 복원과 권한 이전을 별도 PoC로 검증한다.

## 6. 객관적 작업량·리스크 평가

| 담당 | 난이도 | 시간량 | 리스크/완화 | 포트폴리오 결과 |
| --- | ---: | ---: | --- | --- |
| A | 5/5 | 5/5 | Core/Platform 병목. Contract와 Meta UI까지만 직접 구현하고 Feature 구현을 독점하지 않는다. | Unity 아키텍처, UI 구조, 저장·경제, 멀티 권한/세션 |
| B | 4/5 | 4/5 | 연출이 장편 컷씬으로 팽창할 위험. 실시간·짧은·스킵 가능 연출로 제한한다. | 플레이어 게임플레이, UX, 협동 UX, Timeline/Cinemachine |
| C | 5/5 | 5/5 | 월드와 Ally Agent의 동시 과부하. E framework 재사용, Planet 1부터 단계 생산한다. | 레벨/미션 디자인, 다이아제틱 허브, Companion AI |
| D | 5/5 | 5/5 | 기능 증가 전에 데이터 pipeline과 balance 기준을 먼저 만든다. | 타워디펜스 시스템, 성장/경제, data-driven balance |
| E | 5/5 | 5/5 | 적 종류 대량 생산 전 Actor 1종 + Director 최소형을 검증한다. | 적/보스 AI, 호드 Director, AI framework |

### 단계별 상대 비중

| 단계 | A | B | C | D | E |
| --- | ---: | ---: | ---: | ---: | ---: |
| Foundation / Core | 5 | 3 | 3 | 3 | 3 |
| Single Vertical Slice | 4 | 4 | 5 | 5 | 5 |
| Co-op / Online | 5 | 4 | 4 | 4 | 4 |
| Content 확장 | 3 | 4 | 5 | 5 | 5 |
| Polish / Release | 3 | 4 | 4 | 4 | 4 |

초반 A의 부담은 의도된 선택이다. C는 Planet 1 Graybox·Mission/Environment Definition·Hub 동선·Ally profile 초안을 Foundation부터 병행하고, E는 적 1종과 Director 최소형을 먼저 검증한다.

## 7. 누락 방지와 통합 규칙

다음은 별도 역할이 아니라 해당 Feature Owner의 Done과 A의 통합 승인 항목이다.

- Unity Test Framework 기반의 Core/feature test, Play Mode 회귀 확인
- Asset/meta 관리, Scene 충돌 방지, feature별 license/source 확인
- Feature 단위의 Build 검증과 문서/Definition 갱신
- PvP는 ModeRules/계약만 준비하고 실제 `RivalPlayerAgent`는 Deferred 유지

### 보스 Feature 협업

| 산출물 | Owner |
| --- | --- |
| Boss 행동/페이즈 | E |
| Boss arena와 공간 기믹 | C |
| Counter·보상·수치 | D |
| 등장/종료 컷씬과 플레이어 연출 | B |
| 상태·승패·저장 계약 | A |

## 8. 현재 인원 적용

현재 팀원은 2명이다. A~E는 채용 완료 인원이 아니라 **필요한 제품 영역**이다. 두 사람이 겸임할 때도 Core(A)를 임의로 쪼개지 않으며, Planet 1 Vertical Slice에서 필요한 역할만 우선 활성화한다.

1. A Core + Mission/Research UI 기반
2. B Player 기본 전투/Combat HUD 또는 D Defense 최소형
3. C Planet 1 Graybox/미션/Hub 동선
4. E 적 1종 + Director 최소형
5. D 터렛/전력 1종 + 기본 tuning

협동·Steam·Host Migration·Rival Player Agent는 Single loop가 통합 Build에서 검증된 뒤 진행한다.
