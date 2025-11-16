Editor Version은 6000.0.62f1 으로 생성하였습니다.

https://portal.perforce.com/s/downloads?product=Helix%20Visual%20Merge%20Tool%20%28P4Merge%29
여기에서 P4Merge 설치
Select Applications에서
P4Merge만 필요하므로 P4Merge만 선택해도 됨.
나머지는 필요 시 프로그램목록 -> Helix 검색 -> 삭제 하면 됩니다.

.gitattributes, .gitignore 파일은 이미 Unity Project 폴더 안에 있습니다


본인 환경에 맞게 유니티 경로를 기입합니다.
예시 G:/Unity/Hub/Editor/6000.0.62f1/Editor/Data/Tools/UnityYAMLMerge.exe
git config --local merge.unityyamlmerge.driver "G:/Unity/Hub/Editor/6000.0.62f1/Editor/Data/Tools/UnityYAMLMerge.exe merge -p %O %B %A %B"
git config --local merge.unityyamlmerge.name "Unity SmartMerge"

위에서 설정한 유니티 경로로 들어가서
mergespecfile.txt를 열고
unity use "%programs%\YouFallbackMergeToolForScenesHere.exe" "%l" "%r" "%b" "%d"
prefab use "%programs%\YouFallbackMergeToolForPrefabsHere.exe" "%l" "%r" "%b" "%d"
이 부분을 주석처리(#)해주고

밑에 자기 p4merge 설치 경로를 기입하여 붙여넣습니다
unity use "[P4Merge경로]" "%l" "%r" "%b" "%d"
prefab use "[P4Merge경로]" "%l" "%r" "%b" "%d"
예시
unity use "C:\Program Files\Perforce\p4merge.exe" "%l" "%r" "%b" "%d"
prefab use "C:\Program Files\Perforce\p4merge.exe" "%l" "%r" "%b" "%d"


