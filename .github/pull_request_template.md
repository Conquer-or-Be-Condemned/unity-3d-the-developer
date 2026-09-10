## 요약

무엇을 왜 변경했는지 1~3줄로 적어주세요.

- 변경 내용을 작성하세요.

## 관련 Issue

- Closes #
- 관련만 있고 자동으로 닫지 않을 경우: Relates to #

## 개발 영역

- [ ] 공통
- [ ] Planet
- [ ] Spaceship
- [ ] Planet ↔ Spaceship 연결

## 변경 종류

- [ ] 기능
- [ ] Bug 수정
- [ ] Code 구조 개선
- [ ] 성능 개선
- [ ] 설정 / Build / CI
- [ ] 문서
- [ ] Asset

## 담당

- 주 담당: A / B / C / D / E
- 지원 담당:
- 검토 요청자(선택):

> GitHub 승인은 필수가 아니다. 다만 공용 System, Scene 구조, Save, Build 설정처럼 영향 범위가 큰 변경은 관련 담당자에게 검토를 요청한다.

## 핵심 변경 내용

- 핵심 변경 1
- 핵심 변경 2

## 영향받는 항목

- Scene:
- Prefab:
- Script / Assembly:
- Data / Save:
- UI / Audio / Asset:

## 확인 결과

### 직접 확인

- [ ] 해당 기능을 PlayMode에서 확인했다.
- [ ] 관련 Core Loop를 다시 실행했다.
- [ ] 새 Console Error가 없다.
- [ ] 필요하면 Windows Development Build를 확인했다.

확인 순서와 결과:

1.
2.
3.

### CI

- [ ] `.meta`와 프로젝트 구조 검사 통과
- [ ] Unity Compile / Test 통과 또는 실행하지 않은 이유 기록
- [ ] Windows Build 통과 또는 실행하지 않은 이유 기록

## Screenshot / Video

Gameplay, UI, Map, Spaceship, VFX 변경이면 가능하면 첨부합니다.

- Before:
- After:

## 위험과 되돌리는 방법

- 예상되는 영향:
- 문제가 생기면 되돌릴 범위:

## 최종 체크

- [ ] PR은 한 가지 목적에 집중한다.
- [ ] PR 제목이 `feat(area): 내용` 또는 `fix(area): 내용` 형식이다.
- [ ] 임시 Log와 Debug Code를 제거했거나 유지 이유를 적었다.
- [ ] Asset과 `.meta`가 함께 포함됐다.
- [ ] `Library`, `Temp`, `Logs`, `obj`, `.vs`, `UserSettings`를 포함하지 않았다.
- [ ] 관련 문서나 Data 변경이 필요하면 함께 갱신했다.
