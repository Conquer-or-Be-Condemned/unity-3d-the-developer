# Sprint 0 Backlog — 첫 주 작업 목록

기간: 1주
목표: `행성 1 핵심 Prototype`을 시작할 수 있도록 프로젝트를 안정화하고 기획을 확정한다.

> Sprint 0는 M0를 끝내기 위한 1주 예외 Sprint다. 전체 실행 순서는 [DEVELOPMENT_FLOW.md](DEVELOPMENT_FLOW.md)를 따른다. Sprint 1부터는 2주 단위로 운영한다.

## Sprint 목표

> 팀원이 같은 Game Rule을 이해하고, Build 가능한 프로젝트에서 50%와 75% Map 상태를 구현하기 시작할 수 있다.

Sprint 종료 시 완성된 게임 화면이 나올 필요는 없다. 결정되지 않은 Core Rules, 깨진 Build, 불분명한 Map Structure를 남기지 않는 것이 목표다.

## 진행 순서

P0는 전부 중요하지만 한꺼번에 시작하지 않는다.

1. **A단계 — 방향과 프로젝트 안정화:** `PL-001`, `TECH-001`, `CI-001`
2. **B단계 — 병렬 기획:** `MAP-001~002`, `SHIP-001~002`, `TUR-001`, `ENM-001`, `FLOW-001`
3. **C단계 — 검증:** `BAL-001`, `TEST-001`
4. **D단계 — 다음 Sprint 준비:** 남은 작업 시간 안에서 필요한 P1 Task만 수행

A단계가 끝나기 전 Gameplay 기능을 본격 구현하지 않는다. B단계의 결과가 모여야 C단계의 Power Simulation과 Sprint 검토를 진행할 수 있다.

## P0 Tasks

| ID | 영역 | Task | 주 담당 | 지원 | 결과물 | 완료 조건 |
|---|---|---|---|---|---|---|
| PL-001 | 공통 | Project 범위 승인 | A | 전체 팀 | 승인된 `PROJECT_PLAN.md` | 팀원이 이견과 수정 요청을 남기고 A가 Version 0.2 승인 |
| TECH-001 | 공통 | Unity 기반 상태 검증 | E | A | Editor 및 Development Build 결과 | Compile Error 0, Main에서 Stage 1 진입 가능, 정상 종료 가능 |
| CI-001 | 공통 | CI와 `.meta` 검사 확인 | E | D | 성공한 CI 실행 | 누락 `.meta`, 깨진 참조, Build Error를 PR에서 검출 |
| MAP-001 | Planet | 행성 1 전체 Map Graybox | A | C | 100% 전체 Map Image 또는 Unity Graybox | CU, 진입로, Turret, Sector 경계, Player 이동 경로 표시 |
| MAP-002 | Planet | Expansion Stage 정의 | A | C, B | 50/65/75/90/100% Overlay | 단계마다 새 이점 1개와 새 위협 1개가 기록됨 |
| SHIP-001 | Spaceship | Spaceship Gameplay Flow와 필수 Console 정의 | A | B, C, E | Hub에서 준비·출격·귀환하는 흐름 | Navigation, Engineering, Communications, Launch 기능과 Mission 후 변화 정의 |
| SHIP-002 | Spaceship | Spaceship Interior Floor Plan | C | A, D, E | 소형 Hub Floor Plan | 필수 Console 위치, Player 동선, 목표 이동 시간 표시 |
| TUR-001 | Planet | Turret 역할표 작성 | A | D, B | Cannon/Missile/Laser 역할표 | 역할, Power Cost, Range, 강점, 약점, 대응 Monster 정의 |
| ENM-001 | Planet | Monster 역할표 작성 | A | D, B | Default/Tanker/Assassin 역할표 | 각 Monster에 필요한 대응 방법과 첫 등장 Stage 정의 |
| BAL-001 | Planet | Power Paper Simulation | A | B, D | 단계별 유효 조합표 | 각 단계에 서로 다른 Clear 가능 조합 2개 이상 |
| FLOW-001 | 연결 | 10분 Gameplay Flow 작성 | A | B, C, E | 시간대별 Gameplay 흐름 | 준비, Wave, Expansion, Final Defense 예상 시간 존재 |
| TEST-001 | 공통 | Prototype Playtest 질문 작성 | B | A, 전체 팀 | 질문 5~7개 | 설명 없이 이해도와 전략 변화를 확인할 수 있음 |

## P1 Tasks

| ID | 영역 | Task | 주 담당 | 지원 | 결과물 | 완료 조건 |
|---|---|---|---|---|---|---|
| TECH-002 | 공통 | Prototype Scene 분리 | E | A, C | 별도 Test Scene | 기존 Stage 1을 손상시키지 않고 독립 실행 가능 |
| SHIP-003 | Spaceship | Spaceship Hub Graybox Scene | C | D, E | 이동 가능한 소형 Hub | 필수 Console 사이를 목표 시간 안에 이동 가능 |
| SHIP-004 | Spaceship | 공통 Console 상호작용 Prototype | D | C, E | 재사용 가능한 Console Interaction | 접근·상호작용·취소·UI 열기 흐름이 동작 |
| META-001 | 연결 | Mission 왕복 Flow 뼈대 | E | A, C | Hub → Planet → Result → Hub 연결 | 임시 버튼과 Data를 사용해도 전체 왕복이 중단 없이 동작 |
| TUR-002 | Planet | Laser 연결 범위 조사 | D | A | 문제 목록 | Selection UI, Power, Damage, Shutdown, Minimap 누락 항목 확인 |
| DATA-001 | 공통 | Hard-coded Data 목록 작성 | E | D | 이전 대상 목록 | Stage/Wave/Turret/Monster 중 M1에 필요한 항목 식별 |
| UX-001 | Planet | 최소 Power HUD Wireframe | B | A | 흑백 Wireframe | 최대·사용·남은 Power와 실패 이유를 한 화면에서 확인 |
| UX-002 | Spaceship | Mission·Upgrade Console Wireframe | B | A, E | 흑백 Wireframe | Mission 정보, 보상, Upgrade 선택 결과를 이해 가능 |
| PERF-001 | 공통 | 성능 목표 설정 | E | D | Frame Rate 및 개체 수 기준 | Minimum PC, Target FPS, 동시 Monster 수 초안 승인 |
| LEGAL-001 | 공통 | Third-party Asset 목록화 시작 | A | E | License 표 | 기존 음악 및 주요 Asset의 출처와 사용 가능 여부 기록 |

## Planet 1 Level Design 양식

아래 표를 `MAP-002`에서 실제 내용으로 채운다.

| Stage | 개방 Sector | 사용 가능 Turret | Monster 진입로 | 주요 Monster | 새 이점 | 감수할 손해 |
|---|---|---|---|---|---|---|
| 1-1 | 50% |  |  |  |  |  |
| 1-2 | 65% |  |  |  |  |  |
| 1-3 | 75% |  |  |  |  |  |
| 1-4 | 90% |  |  |  |  |  |
| 1-5 | 100% |  |  |  |  |  |

## Spaceship Hub Design 양식

| 공간 / Console | Player 행동 | 정보 / 선택 | Mission 전후 변화 | 목표 이동 시간 |
|---|---|---|---|---:|
| Navigation Console | Mission 선택 | 행성 확장률, 예상 Threat | 새 Sector 표시 | 10초 이내 |
| Engineering Console | Upgrade 적용 | Power 또는 Turret Upgrade 1개 | 장치와 수치 변화 | 10초 이내 |
| Communications Console | Log 확인 | 선발대·Story 정보 | 새 Message 표시 | Optional |
| Launch Point | 행성 이동 | 선택 Mission 확인 | Launch Sequence | 10초 이내 |

## Turret 역할표 양식

| Turret | 주 역할 | Power Cost | Range | 강한 상대 | 약한 상황 | 켜야 하는 이유 |
|---|---|---:|---:|---|---|---|
| Cannon |  |  |  |  |  |  |
| Missile |  |  |  |  |  |  |
| Laser/Railgun |  |  |  |  |  |  |

## Monster 역할표 양식

| Monster | 전투 역할 | Target | 필요한 대응 | 강한 상대 | 약한 상대 | 첫 등장 |
|---|---|---|---|---|---|---|
| Default |  |  |  |  |  |  |
| Tanker |  |  |  |  |  |  |
| Assassin |  |  |  |  |  |  |

## Prototype Playtest 질문

1. Turret을 켤 수 없을 때 그 이유를 스스로 알았는가?
2. Wave가 바뀐 뒤 Active Turret Build를 바꾸었는가? 왜 바꾸었는가?
3. Map Expansion 후 새롭게 가능해진 행동은 무엇이었는가?
4. Expansion으로 더 어려워진 점은 무엇이었는가?
5. 이전 Sector의 Turret을 Expansion 후에도 사용했는가?
6. Player Character가 직접 싸워야 했던 순간이 있었는가?
7. 항상 켜 두고 싶은 Turret 하나가 있었는가? 있었다면 이유는 무엇인가?

## Sprint 0 통과 기준

- P0 Tasks가 모두 완료됐다.
- Compile Error와 CI Failure가 없다.
- 행성 1 전체 Map과 Expansion Boundary가 승인됐다.
- 소형 Spaceship Hub의 필수 기능과 Player Flow가 승인됐다.
- Turret과 Enemy의 Role이 수치보다 먼저 정의됐다.
- 팀원이 다음 Sprint에서 자신이 맡을 Feature와 Task를 알고 있다.
- M1 Out of Scope가 명시돼 있다.
