# The Developer Project Docs

수정일: 2026-09-10

이 폴더는 리뉴얼 중인 `The Developer`의 기획과 개발 기준을 관리한다. 개발 순서가 헷갈리면 항상 `DEVELOPMENT_FLOW.md`부터 확인한다.

## 처음 읽을 문서

1. [DEVELOPMENT_FLOW.md](DEVELOPMENT_FLOW.md) — 전체 개발 단계, Sprint, Task, 통합 순서
2. [PROJECT_PLAN.md](PROJECT_PLAN.md) — 게임 정체성, 제작 범위, 핵심 규칙, Milestone과 결정 기록
3. [SPRINT_0_BACKLOG.md](SPRINT_0_BACKLOG.md) — 지금 완료해야 할 Task와 Sprint 0 통과 기준
4. [TEAM_WORKFLOW.md](TEAM_WORKFLOW.md) — Planet/Spaceship별 A~E 역할, Git/Unity 규칙, 착수·완료 조건

```text
PROJECT_PLAN에서 목표와 범위 결정
→ DEVELOPMENT_FLOW에서 순서와 통과 기준 확인
→ SPRINT BACKLOG에서 담당자와 Task 확정
→ TEAM_WORKFLOW 규칙으로 구현·검토·Merge
→ 통합 Build와 Playtest
→ 통과하면 다음 Sprint 또는 Milestone
```

## 문서 우선순위

내용이 충돌하면 다음 순서로 판단한다.

1. 최근에 승인된 `Decision Log`(결정 기록)
2. `PROJECT_PLAN.md`의 제작 범위와 핵심 규칙
3. `DEVELOPMENT_FLOW.md`의 실행 순서와 통과 기준
4. 현재 Sprint Backlog의 확정된 작업
5. `TEAM_WORKFLOW.md`의 협업 규칙
6. 기존 README와 과거 회의록

Game Rule이나 제작 범위를 변경할 때는 회의에서 말로만 끝내지 않고 `PROJECT_PLAN.md`의 Decision Log를 갱신한다. 실행 순서가 바뀌면 `DEVELOPMENT_FLOW.md`, 당장 할 일이 바뀌면 현재 Sprint Backlog도 함께 갱신한다.

## 어디에 기록할까?

| 상황 | 기록 위치 |
|---|---|
| 새 아이디어 | Product Backlog(나중에 검토할 일 목록) |
| 승인된 Game Rule 또는 제작 범위 변경 | `PROJECT_PLAN.md` Decision Log |
| 개발 순서나 통과 기준 변경 | `DEVELOPMENT_FLOW.md` |
| 이번 Sprint의 구체적인 일 | 현재 Sprint Backlog |
| Git, 검토, 완료 규칙 변경 | `TEAM_WORKFLOW.md` |
| Bug 발견 | Issue Tracker의 Bug 기록 양식 |

## 용어 사용 기준

게임 개발에서 실제로 자주 쓰는 용어는 유지하되, 작업 관리 용어는 쉬운 한국어를 함께 쓴다.

- Vertical Slice
- Graybox / Blockout
- Core Loop / Meta Loop
- Milestone (중간 목표)
- Sprint (짧은 개발 주기) / Backlog (할 일 목록)
- Epic / Feature / Task / Bug
- 담당자(Owner)
- Playtest
- Build / CI
- Hub
- 범위(Scope) / 제외 범위(Out of Scope)
- 착수 조건(Definition of Ready) / 완료 조건(Definition of Done)
- 선행 작업(Dependency)
- 통과 기준(Exit Criteria)

`Graybox`, `Vertical Slice`, `Sprint`, `Backlog`, `Build`, `Playtest`, `PR`, `CI`처럼 팀에서 그대로 사용할 용어는 유지한다. 그 외 어려운 관리 용어는 한국어를 우선하고 필요할 때만 영문을 괄호로 덧붙인다.
