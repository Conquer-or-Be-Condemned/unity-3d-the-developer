# The Developer 프로젝트 계획서

상태: Draft 0.1

기준일: 2026-09-10

## 1. 프로젝트 목적

`The Developer`를 기존의 단순 호드 방어 게임에서 다음 정체성을 가진 게임으로 재구성한다.

> 플레이어가 직접 전투하면서, 선발대가 행성에 미리 설치한 수많은 터렛 중 일부만 제한된 전력으로 가동하고, 스테이지마다 확장되는 전장을 방어하는 2D 탑다운 액션 타워디펜스.
>

# **상단         절대         임의          수정         금지**

---

> 이 문서는 `무엇을 왜 만들지`를 정의한다. 실제 개발 순서와 Sprint 운영은 [DEVELOPMENT_FLOW.md](DEVELOPMENT_FLOW.md), 팀원별 역할은 [TEAM_WORKFLOW.md](TEAM_WORKFLOW.md)를 따른다.

## 첫 번째 제작 목표는 전체 게임이 아니라 이 설명이 실제 플레이에서도 재미있고 이해되는지를 증명하는 `Planet 1 Vertical Slice`다.

## 2. 현재 프로젝트 상태

### 현재 저장소에는 다음 기반이 존재한다.

- Unity `6000.3.23f1`
- 플레이어 이동, 사격, 체력, 폭탄
- Control Unit 체력과 최대 전력
- 캐논 및 미사일 터렛과 개별 활성화
- Laser Turret 코드와 Stage 1 Prefab 인스턴스
- 기본, 탱커, 암살자, 원거리, 보스 계열 몬스터
- 웨이브, 스포너, 게임오버, 스테이지 클리어
- Main, Opening, Stage Select, 3개 행성, Ending Scene
- 미니맵, 상점, 설정, 대화 및 일부 오디오

---

### 현재 구현은 2024년 2학기 프로젝트의 완성 구조를 기반으로 하므로, 새 기획을 그대로 수용하기에는 다음 문제가 있다.

- 맵 확장 개념이 없다.
- 몬스터 및 보스의 알고리즘, 코드 수정이 필요하다.
- Planet과 Wave 정보가 Code에 hard-coded돼 있다.
- `TowerManager`가 캐논과 미사일의 구체 타입을 직접 판별한다.
- Laser Turret은 Shared Selection UI와 Wave 종료 처리에 완전히 Integration되지 않았다.
- Turret 계열마다 유사 Code가 중복돼 있어 종류를 늘릴수록 수정 비용이 증가한다.
- 기존 3개 Planet Scene은 새로운 `1-1 ~ 1-5` 구조를 기준으로 제작되지 않았다.
- 기존 Audio 일부는 상업적 사용 권리를 재확인해야 한다.

기존 Scene과 Feature는 당장 삭제하거나 전면 재작성하지 않는다. 별도의 Core Prototype에서 새 구조를 검증한 후 필요한 부분만 점진적으로 교체한다.

## 3. 플레이어 경험 목표

### 플레이어가 다음 감정을 순서대로 느끼는 것을 목표로 한다.

1. 전장에 수많은 터렛이 있지만 모두 켤 수 없다는 긴장감
2. 적 구성과 공격 방향을 보고 전력을 배분하는 계획의 재미
3. 전력이 없는 방어선으로 직접 달려가 빈틈을 메우는 액션
4. 새 구역이 열리며 강력한 선택지가 생기는 기대감
5. 동시에 방어 면적과 적의 진입로가 늘어나는 부담감
6. 마지막에는 자신이 운용한 방어망 전체로 행성을 지켜냈다는 성취감

## 4. 핵심 기획 원칙

### 4.1 Power Budget은 공간에 펼쳐진 Loadout이다

터렛은 건설하거나 이동하지 않는다. 플레이어는 미리 설치된 터렛 중 현재 전투에 필요한 조합을 선택한다. 최대 전력은 장비창의 코스트 제한과 같은 역할을 한다.

### 4.2 Map Expansion은 전술을 바꿔야 한다

맵이 넓어질 때마다 다음 두 항목이 반드시 함께 생긴다.

- 새롭게 할 수 있는 것 하나 이상: 신규 터렛, 외곽 요격선, 지름길, 레이더(미확장 구역 몬스터 감지 불가,미니맵에 안뜨게), 특수 시설 등
- 새롭게 감당할 것 하나 이상: 신규 스폰 방향, 긴 이동 거리, 다중 전선, 특수 몬스터 등

면적과 배경만 늘어나는 Expansion은 유효한 Content로 인정하지 않는다.

아직 확장(발견)되지 않는 지역에 플레이어가 접근하면 패널티 주기(ex. 느려지기, RPS 줄이기, Mis-fire 늘리기)

### 4.3 Player와 Turret은 서로의 약점을 보완한다

터렛이 모든 전선을 자동으로 해결해서도 안 되고, 플레이어 화력만으로 전력 선택을 무시할 수 있어서도 안 된다. 플레이어는 전력이 배정되지 않은 구역과 긴급 상황을 처리하는 이동형 화력이다.

### 4.4 Choice는 읽을 수 있어야 한다

플레이어는 터렛의 소비 전력, 역할, 사거리, 현재 상태와 적의 주요 특성을 이해할 수 있어야 한다. 정보 없이 정답을 찍게 만드는 난이도는 사용하지 않는다.

### 4.5 Spaceship은 Core Meta Loop다

Spaceship은 장식용 Lobby가 아니라 다음 Mission을 이해하고 준비하는 Gameplay Space다. Player는 Spaceship 내부를 직접 이동하며 Mission을 선택하고, Research·Progression 결과를 적용하고, 선발대와 Planet에 관한 정보를 확인한 뒤 출격한다.

Vertical Slice에서는 거대한 함선 전체가 아니라 아래 기능을 가진 작은 Interior만 제작한다.

- 항법 콘솔: 행성 상태와 확장률 확인, 임무 선택
- 공학 콘솔: 전력 또는 터렛 관련 성장 1회 적용
- 통신/기록 콘솔: 선발대 기록과 다음 임무 정보 확인
- 출격 지점: 행성으로 이동

각 장소는 기능을 가져야 하며, 메뉴를 열기 위해 빈 복도를 오래 걷게 만들지 않는다.

## 5. Core Gameplay Loop — 핵심 플레이 흐름

```
Spaceship Hub에서 Planet 상태와 Mission 확인
→ Research·Power·Turret 준비
→ Planet으로 Launch
→ 새 Sector 또는 Wave 정보 확인
→ 적의 종류와 공격 방향 예측
→ 최대 전력 안에서 터렛 활성 조합 선택
→ 플레이어 직접 전투와 제한적인 전력 재배치
→ Control Unit 방어
→ Stage Clear
→ Map Expansion과 새로운 Defense Option 개방
→ Spaceship Hub로 귀환해 Result 확인
```

### 승리 조건

- 해당 스테이지의 모든 필수 웨이브 또는 목표를 완료한다.
- 최종 스테이지에서는 행성 보스를 처치한다.

### 실패 조건

- 플레이어 체력이 0이 된다.
- Control Unit 체력이 0이 된다.

## 6. Vertical Slice 제작 범위

최종 구조는 행성 하나를 `1-1 ~ 1-5`로 확장하는 안을 유지한다. 하지만 첫 검증판은 제작량을 줄이기 위해 세 구간만 사용한다.

- Phase 1: Map 50%
- Phase 2: Map 75%
- Phase 3: Map 100%
- Target Playtime: 15~20분

### 포함 범위

- 플레이어 무기 1종
- Control Unit 1개
- Turret 3종: Cannon, Missile, Laser/Railgun
- Enemy 3종: Default, Tanker, Assassin
- Boss 1종
- Planet 1개와 Full Map 1개
- Map Expansion 2회
- Power HUD와 Turret 개별 ON/OFF
- 직접 이동 가능한 소형 Spaceship Interior 1개
- Navigation·Engineering·Communications Console의 최소 Interaction
- Spaceship → Planet → Spaceship → Next Mission Flow
- 귀환 후 적용하는 Upgrade Choice 1개
- Start, Success, Fail, Retry Flow
- 최소한의 Tutorial과 Context 설명

### 제외 범위

- Online Co-op 및 SOS
- 두 번째 Planet
- Turret 건설과 위치 이동
- Sector 단위 일괄 Power Control
- 다수의 Player Weapon
- 대형 Research Tree와 Meta Progression
- Random 또는 Procedural Map
- Multi-deck Spaceship과 Ship Customization
- Spaceship Combat, Crew Management, Maintenance Simulation
- 전체 Story와 Cutscene
- 신규 Turret 4종 이상

## 7. 핵심 Prototype 규칙

아래 수치는 최종 밸런스가 아니라 선택 구조를 검증하기 위한 기준이다.

- 종이 기획상의 최대 전력: 100
- 캐논: 20
- 미사일: 30
- 레이저/레일건: 50
- Pre-Wave Power 변경: 자유
- In-Wave Power 변경: 가능하되 Recovery/Transition Delay 또는 횟수 제한 존재
- Deactivated Turret의 Power Return 목표 시간: 2~4초
- 하나의 조합으로 모든 상황을 해결하지 못하도록 적 조합 구성
- 맵 확장 후에도 이전 구역과 내곽 터렛이 최종 방어선으로 유효해야 함

Turret 개수가 적은 Vertical Slice에서는 개별 조작만 사용한다. 외부 Playtest에서 반복 클릭이 명확한 문제로 관찰되거나 한 Map의 Turret 수가 15~20개를 넘을 때 Sector Control을 다시 검토한다. Sector Control은 후반 Stage에서 Skill로 제공하는 방안도 검토한다.

## 8. Planet 1 Content 구성

| 단계 | 개방률 | 가르칠 내용 | 새 이점 | 새 위협 |
| --- | --- | --- | --- | --- |
| 1-1 | 50% | 이동, 사격, 터렛 ON/OFF, 전력 제한 | 중앙 캐논 및 미사일 방어선 | 기본 적, 2개 진입로 |
| 1-2 | 65% | 외곽과 내곽 전력 선택 | 외곽 요격선 또는 보조 시설 | 신규 진입로, 빠른 적 |
| 1-3 | 75% | 고전력 터렛 선택 | 레이저/레일건 방어선 | 탱커와 혼합 웨이브 |
| 1-4 | 90% | 여러 전선 사이의 재배분 | 지름길 또는 전력 효율 시설 | 동시다발 공격 |
| 1-5 | 100% | 전체 시스템 종합 | 행성 전체 방어망 | 보스와 다방향 공격 |

Vertical Slice에서는 `1-1`, `1-3`, `1-5`에 해당하는 세 State만 먼저 제작한다. 통과 기준을 충족한 뒤 중간 Stage를 채운다.

## 9. Milestones — 개발 단계

날짜보다 각 Milestone의 통과 기준(Exit Criteria)을 우선한다. 기간은 소규모 파트타임 팀 기준 예상치이며 실제 작업 가능 시간에 따라 조정한다.

### M0. Baseline Stabilization & Design Lock — 1주

결과물:

- Unity 에디터에서 컴파일 오류 0
- Windows Development Build 실행
- CI 통과와 `.meta` 누락 검사
- 행성 1 Full Map Graybox 설계(검증이기 때문에 예쁠 필요 없음)
- 50/65/75/90/100% 구역 경계
- Spaceship Interior 동선과 필수 Console Graybox 설계
- `우주선 → 1-1 → 귀환 → 성장 → 1-2` 흐름도
- 터렛·몬스터 역할표와 전력 수치 초안
- 보류 기능 목록

통과 기준:

- 모든 팀원이 게임의 핵심을 같은 문장으로 설명한다.
- 종이 시뮬레이션에서 최소 두 개의 유효한 전력 조합이 나온다.
- `main` Branch와 CI가 정상 상태다.
- 8주 동안 바꾸지 않을 Vertical Slice Scope가 승인된다.

### M1. Core Prototype — 2주

결과물:

- 별도 Graybox Test Scene
- 50% → 75% → 100% Map Expansion
- Cannon, Missile, Laser의 Shared Power Control
- Default, Tanker, Assassin Wave
- Expansion 후 새로운 Spawn Direction
- Minimum Power HUD
- 소형 Spaceship Hub Graybox와 Navigation Console
- Spaceship에서 Planet으로 Launch하고 Result 후 Return하는 Scene Flow
- Expansion, Invincibility, Wave Skip Debug Tools

통과 기준:

- 10분 Core Loop를 5회 연속 Progression Blocker와 Console Error 없이 완주한다.
- 서로 다른 두 Turret Build로 Clear할 수 있다.
- 한 Build가 모든 Wave의 명백한 정답이 아니다.
- Expansion 전 Locked Sector에서 Collision, Projectile, Spawn이 새지 않는다.
- Expansion 후 Camera, Collision, Spawn, Minimap이 정상 Update된다.
- Expansion이 Attack Direction과 Power Allocation을 실제로 바꾼다.
- Spaceship에서 Mission을 선택해 Launch하고 Return하는 Flow가 중단 없이 동작한다.

### M2. Planet 1 Vertical Slice — 4~6주

결과물:

- 15~20분 분량의 Release Quality에 가까운 Spaceship·Planet 왕복 Gameplay
- 기능이 있는 소형 Spaceship Interior와 Return 후 변화
- Expansion Sequence와 선발대 Environmental Storytelling
- Boss, Result Screen, Fail 및 Retry
- 읽기 쉬운 Power 및 Turret UI
- 실제 Tileset, 핵심 VFX/SFX/BGM
- Basic Options와 Minimum Save
- Automated Build Validation

통과 기준:

- 외부 Tester 5명 중 4명이 설명 없이 Turret을 켜고 Power Limit을 이해한다.
- 5명 중 4명이 Map Expansion과 Power Allocation을 게임의 특징으로 기억한다.
- 5명 중 4명이 Spaceship에서 Next Action과 Launch 방법을 설명 없이 찾는다.
- Expansion 후에도 이전 Sector가 전술적으로 사용된다.
- Target PC에서 정한 Frame Rate 하한을 지킨다.
- Progression Blocker, Crash, Save Corruption이 없다.

### M3. Content Pipeline — 2주

결과물:

- Shared Turret Interface와 Data Structure
- Data-driven Stage, Wave, Spawn Group
- `MapSector` 또는 동등한 Sector Unlock Structure
- Code 수정 없이 수치를 조정할 수 있는 Data Asset
- Scene Required Reference, Spawn, Turret, `.meta` Validation Tool
- Planet Production Checklist

통과 기준:

- 기존 Code를 바꾸지 않고 Graybox Planet을 1~2일 안에 만들 수 있다.
- Laser 이후 새로운 Turret을 추가해도 `TowerManager`에 Type-specific Branch를 추가하지 않는다.
- Spawn이나 Scene Reference 누락을 Build 전에 발견한다.

### M4. Alpha Content Production — Scope 확정 후 산정

- Planet 1을 1-1 ~ 1-5로 완성
- Vertical Slice Metrics가 통과한 경우에만 추가 Planet 제작
- Planet당 신규 System은 최대 1개
- 먼저 Graybox로 검증하고 이후 Final Art 적용
- 모든 예정 Content가 처음부터 끝까지 Playable한 상태가 Alpha 완료

### M5. Beta & Polish — 3~4주

- 기능 추가 중단
- 난이도, 전력 비용, 적 조합 조정
- 입력, UI, 가독성, 접근성 개선
- Performance Optimization과 장시간 Playtest
- Crash 및 Major Bug 0
- Audio와 Third-party Asset의 License 최종 확인

### M6. Release Candidate — 1~2주

- Platform별 Final Build
- Clean Install Test
- Save Compatibility 및 Reset Test
- Store Page, Credits, License Document
- Release Candidate Tag와 복구 가능한 Backup

## 10. 품질 기준

### Core Fun

- 플레이어가 적 구성에 따라 터렛 조합을 변경한다.
- 최소 두 개 이상의 조합이 유효하다.
- 플레이어 직접 전투가 승패에 영향을 준다.
- 맵 확장 전후의 플레이가 다르게 느껴진다.

### 사용성

- 설명 없이 전력 부족 이유를 알 수 있다.
- 터렛의 ON/OFF, 소비 전력, 사거리를 빠르게 확인할 수 있다.
- 새로 개방된 구역과 진입로를 놓치지 않는다.
- 우주선에서 임무 선택, 성장, 출격 동선을 헤매지 않는다.

### 기술 품질

- 컴파일 오류와 진행 불가 버그 0
- CI에서 누락된 `.meta`와 깨진 참조 검출
- 목표 하드웨어 프레임 하한 유지
- 세 번 연속 처음부터 끝까지 정상 완주

## 11. 위험 요소와 대응

| Risk | Signal | Mitigation |
| --- | --- | --- |
| 맵 확장이 시각 변화에 그침 | 플레이어가 무엇이 달라졌는지 설명하지 못함 | 확장마다 새 이점과 새 진입로를 필수화 |
| 전력 선택이 하나의 정답으로 굳음 | 항상 같은 터렛만 가동 | 적 조합, 사거리, 지형을 조정하고 두 조합 통과 기준 적용 |
| 직접 전투가 무의미함 | 터렛만 켜고 기다림 | 터렛 사각, 긴급 적, 플레이어 전용 대응 추가 |
| 직접 전투가 터렛을 압도함 | 전력 관리 없이 클리어 | 플레이어 DPS와 탄약, 위험도를 조정 |
| 오래된 구조에서 기능 추가 비용 폭증 | 터렛마다 Manager 분기 추가 | M1은 최소 수정, M3에서 공통 데이터 구조 확립 |
| Unity Scene 충돌 | 같은 Scene을 여러 명이 동시에 수정 | Sector Prefab / Additive Scene 구조와 Scene 담당제 적용 |
| `.meta` 누락 | CI에서 반복 실패 | Visible Meta Files, Unity 내 이동, PR 검사 |
| 범위 팽창 | 온라인, 대형 우주선, 신규 행성이 동시에 진행 | M2 통과 전 보류 목록 변경 금지 |
| 우주선이 걷는 메뉴로만 느껴짐 | 콘솔 이동이 귀찮고 바로 출격하고 싶다는 반응 | 핵심 콘솔을 가깝게 배치하고 귀환 후 눈에 보이는 변화 제공 |
| 에셋 라이선스 문제 | 출시 직전 음악 교체 필요 | M0에서 출처 목록화, M5 전 교체 완료 |
| 대량 몬스터 성능 | 스폰 시 끊김, GC 증가 | 목표 개체 수 설정, 풀링과 프로파일링을 M2에서 수행 |

## 12. 아직 결정하지 않은 항목

다음 항목은 Core Prototype과 Playtest 결과를 보고 결정한다.

- 최종판에서 모든 서브 스테이지마다 우주선으로 복귀할지, 주요 단계에서만 복귀할지 여부
- 이전 스테이지의 터렛 상태와 피해를 다음 스테이지에 유지할지 여부
- 전투 중 전력 전환의 정확한 지연과 제한
- 터렛의 파괴 및 수리 여부
- 최종 행성 수와 전체 플레이 시간
- 구역 단위 전원 제어 필요 여부
- Online Co-op의 형태와 제작 시점

## 13. 결정 기록 (Decision Log)

| Date | Decision | Reason | Impact |
| --- | --- | --- | --- |
| 2026-09-10 | 첫 목표를 Planet 1 Vertical Slice로 제한 | Core Fun 검증 전 Content 확대 방지 | Online, 추가 Planet, 대형 Progression System 보류 |
| 2026-09-10 | 터렛은 선발대가 미리 설치 | 건설보다 제한 전력 운용을 핵심으로 유지 | 맵 설계가 터렛 선택지를 결정 |
| 2026-09-10 | 초기 검증에서는 개별 터렛 조작 사용 | 현재 구현 활용 및 불필요한 UI 복잡도 방지 | Sector Control은 Playtest 후 재검토 |
| 2026-09-10 | Vertical Slice에 소형 Spaceship Interior와 Round-trip Loop 포함 | Spaceship이 게임의 기반이므로 Combat만으로 Final Experience를 대표할 수 없음 | 대형 Ship은 제외하고 Navigation·Engineering·Communications 기능만 우선 제작 |
