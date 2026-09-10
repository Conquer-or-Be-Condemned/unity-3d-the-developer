# The Developer — Project Documentation

수정일: 2026-09-11

이 폴더는 Unity 6 기반으로 리뉴얼하는 `The Developer`의 제품 기획, Core 아키텍처, 개발 운영 기준을 보관한다.
레거시 코드는 기능·수치·연출의 참고 자료이며, 앞으로의 시스템 기능은 새 Core에 점진적으로 이식한다.

## 먼저 읽을 문서

새 팀원 또는 새 기능 담당자는 아래 순서로 읽는다.

1. [PROJECT_PLAN.md](PROJECT_PLAN.md) — 게임 정체성, Planet 1 Vertical Slice, 현재 제품 범위와 Decision Log
2. [system-rearchitecture-charter.md](system-rearchitecture-charter.md) — 레거시 교체 원칙, 새 Core의 경계, 멀티플레이를 고려한 설계 방향
3. [milestones-and-core-design.md](milestones-and-core-design.md) — M1 Core부터 Release까지의 목표, 수명주기, 의존성, 데이터 계약
4. [naming-and-architecture-conventions.md](naming-and-architecture-conventions.md) — 폴더/asmdef 방향, 네임스페이스, 클래스와 인터페이스 명명 규칙
5. [TEAM_ROLE_OWNERSHIP.md](TEAM_ROLE_OWNERSHIP.md) — 최종 5개 제품 영역, AI 분해, 협업 경계, 작업량·리스크 평가
6. [TEAM_WORKFLOW.md](TEAM_WORKFLOW.md) — Sprint, Git/Unity 규칙, Definition of Ready/Done, 리뷰·통합 방식
7. [DEVELOPMENT_FLOW.md](DEVELOPMENT_FLOW.md) — 현재 단계의 실행 순서와 Milestone 통과 기준
8. [SPRINT_0_BACKLOG.md](SPRINT_0_BACKLOG.md) — 현재 Sprint의 구체 Task와 완료 조건

레거시 구조와 목표 구조의 대응을 확인해야 할 때는 [architecture-rebuild-notes.md](architecture-rebuild-notes.md)를 참고한다.
팀 충원 목적의 외부 공개 문서는 [team-recruitment-proposal.md](team-recruitment-proposal.md)다.

```text
PROJECT_PLAN에서 무엇을 만들지 결정
        ↓
System Charter / Core Design에서 어떤 구조로 만들지 확인
        ↓
Naming Convention과 Role Ownership으로 경계·담당 확정
        ↓
Development Flow와 Sprint Backlog로 이번 작업 확정
        ↓
Team Workflow의 Ready/Done 규칙으로 구현·리뷰·통합
        ↓
통합 Build·Playtest 후 Milestone 통과 여부 결정
```

## 문서별 Source of Truth

문서가 충돌할 때는 문서의 종류에 따라 아래 기준을 적용한다.

| 질문 | 우선 문서 | 보조 문서 |
| --- | --- | --- |
| 무엇을 만들고, 무엇을 미룰 것인가? | `PROJECT_PLAN.md`의 승인된 Decision Log | `DEVELOPMENT_FLOW.md` |
| 레거시를 어떻게 교체하고 어떤 의존성을 허용하는가? | `system-rearchitecture-charter.md` | `milestones-and-core-design.md`, `architecture-rebuild-notes.md` |
| M1~Release에서 무엇을 언제 검증하는가? | `milestones-and-core-design.md` | `DEVELOPMENT_FLOW.md` |
| 클래스/폴더/계약을 무엇이라 부르는가? | `naming-and-architecture-conventions.md` | Core Design 문서 |
| 누가 어떤 제품 영역과 AI를 소유하는가? | `TEAM_ROLE_OWNERSHIP.md` | `TEAM_WORKFLOW.md` |
| Sprint, Git, 리뷰, 착수/완료를 어떻게 운영하는가? | `TEAM_WORKFLOW.md` | `DEVELOPMENT_FLOW.md` |
| 지금 당장 무엇을 하는가? | 현재 Sprint Backlog | `DEVELOPMENT_FLOW.md` |

승인된 제품 범위나 시스템 규칙을 바꾸면 말로만 끝내지 않는다. `PROJECT_PLAN.md`의 Decision Log와
영향받는 Source of Truth 문서를 같은 변경에서 갱신한다.

## 현재 제품/기술 방향 요약

- 탑다운 2D 호드 타워디펜스: 플레이어 전투, 터렛/전력 방어, 적 웨이브, 구역 확장
- 우주선 기반 다이아제틱 허브: 미션, 연구, 성장, 결과를 공간과 UI로 연결
- 싱글플레이 Core loop를 먼저 안정화한 뒤 Steam 기반 2인 협동을 확장
- 협동은 Invite/Random Match/SOS를 목표로 하며, 이탈 플레이어는 아군 AI로 대체 가능해야 함
- Host migration과 PvP/`RivalPlayerAgent`는 계약·PoC를 우선하고 실제 기능은 **Deferred** 범위로 관리
- Economy/Profile은 backend 검증을 목표로 하며, local에는 versioned binary cache만 둠

## 문서 변경 규칙

| 변경 종류 | 함께 갱신할 문서 |
| --- | --- |
| 게임 규칙, 범위, Deferred 결정 | `PROJECT_PLAN.md` Decision Log |
| Core lifecycle, data contract, 외부 SDK 경계 | System Charter, Core Design, 필요 시 Naming Convention |
| 역할/협업 경계, AI Owner 변경 | `TEAM_ROLE_OWNERSHIP.md`, `TEAM_WORKFLOW.md` |
| Sprint 목표·작업·완료 조건 변경 | 현재 Sprint Backlog, `DEVELOPMENT_FLOW.md` |
| UI/Scene/Prefab 작성 규칙 변경 | `TEAM_WORKFLOW.md`, 필요 시 Naming Convention |

`Docs/`는 기본적으로 로컬 설계 노트 공간으로 ignore한다. 팀의 합의 기준(Source of Truth)이 된 문서는
명시적으로 Git 추적 대상으로 승격한다. 추적 여부는 문서의 중요도와 팀 공유 필요성으로 결정하며,
무심코 모든 초안을 원격 저장소에 올리지 않는다.

## 용어 사용 기준

게임 개발에서 자주 쓰는 용어는 유지하되, 작업 관리 용어에는 쉬운 한국어를 병기한다.

- `Vertical Slice`, `Graybox` / `Blockout`, `Core Loop`, `Meta Loop`, `Hub`
- `Milestone`, `Sprint`, `Backlog`, `Epic`, `Feature`, `Task`, `Bug`
- `Owner`, `Definition of Ready`, `Definition of Done`, `Exit Criteria`
- `Command`, `Event`, `Snapshot`, `Authority`, `Adapter`, `Presenter`, `Definition`, `Profile`
- `Build`, `Playtest`, `PR`, `CI`

새 용어를 도입할 때는 [naming-and-architecture-conventions.md](naming-and-architecture-conventions.md)의
규칙을 우선한다.
